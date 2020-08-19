using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace forStudents
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
        public string surnamename { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            connection.Open();
            ConnectionToSql();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd studentWindow = new StudentAdd();
            studentWindow.connection = connection;
            studentWindow.Owner = this;
            studentWindow.Show();  
        }
        private void ConnectionToSql() {
            
            try
            {
                SqlCommand command = new SqlCommand("select S.Name, S.Surname, G.Name from Student S Join Groups G On S.idGroup=G.id", connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                        lbStudents.Items.Add($"{reader["Name"]}\t {reader["Surname"]}\t {reader[2]}");

                reader.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            connection.Close();
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            lbStudents.Items.Clear();
            ConnectionToSql();
        }
    }
}

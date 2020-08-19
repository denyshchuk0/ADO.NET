using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        
        public SqlConnection connection { get; set; }
        List<int> IdGroup = new List<int>();
        public StudentAdd()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand($"insert into Student values('{tbName.Text}','{tbSurname.Text}',{IdGroup[cbGroups.SelectedIndex]})", connection);

                command.ExecuteNonQuery();
            }
            catch (SqlException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand("select Name, Id from Groups", connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    cbGroups.Items.Add(reader["name"]);
                    IdGroup.Add(Convert.ToInt32(reader["id"]));
                }
            reader.Close();
        }
    }
}

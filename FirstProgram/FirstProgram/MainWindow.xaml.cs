using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirstProgram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static SqlConnection connection = new SqlConnection();
        public MainWindow()
        {
            InitializeComponent();
            btnIndecator.Background = new SolidColorBrush(Colors.Red);
        }

        private void ChangeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChangeType.SelectedIndex == 0)
            {
                LPass.IsEnabled = Password.IsEnabled = true;
                LLogin.IsEnabled = Login.IsEnabled = true;
            }
            else
            {
                Adress.Text = @"(localdb)\MSSQLLocalDB";
                LPass.IsEnabled = Password.IsEnabled = false;
                LLogin.IsEnabled = Login.IsEnabled = false;
            }
        }

        private void btnAcept_Click(object sender, RoutedEventArgs e)
        {
            string connectionStringSQL;

            if (ChangeType.SelectedIndex == 0)
                connectionStringSQL = $"Data Source={Adress.Text};" +
                                      $"Initial Catalog=master;" +
                                      $"Integrated Security=false;" +
                                      $"User Id={Login.Text};" +
                                      $"Password={Password.Password}";
            else
                connectionStringSQL = $"Data Source={Adress.Text};" +
                                   "Initial Catalog=master;" +
                                   "Integrated Security=True";
            connection.ConnectionString = connectionStringSQL;

            try
            {
                connection.Open();
                btnIndecator.Background = new SolidColorBrush(Colors.Green);
                SqlCommand command = new SqlCommand("select Name from sys.sysdatabases", connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    while (reader.Read())
                    {
                        cbDatabases.Items.Add(reader["Name"]);
                    }
                reader.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cbDatabases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbTable.Items.Clear();
            SqlCommand showTable = new SqlCommand($"use {cbDatabases.SelectedItem.ToString()}; select Name From sys.tables", connection);

            CompliteSqlCommand(lbTable, showTable);
        }
        private void CompliteSqlCommand(ListBox lb, SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                    lb.Items.Add(reader["Name"]);

            reader.Close();
        }
        private void lbTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbTableContent.Items.Clear();
            if (lbTable.SelectedItem == null)
                return;

            SqlCommand showTableContent = new SqlCommand($" select * From {lbTable.SelectedItem.ToString()} ", connection);
            CompliteSqlCommand(lbTableContent, showTableContent);
        }
        private void Win_Closing(object sender, System.ComponentModel.CancelEventArgs e) => connection.Close();
        private void btnExit_Click(object sender, RoutedEventArgs e) => Win.Close();
    }
}

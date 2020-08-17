using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace FirstProgram
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChangeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ChangeType.SelectedIndex == 0)
            {
                LPass.IsEnabled=Password.IsEnabled = true;
                LLogin.IsEnabled=Login.IsEnabled = true;
            }
            else
            {
                Adress.Text = @"(localdb)\MSSQLLocalDB";
                LPass.IsEnabled=Password.IsEnabled = false;
                LLogin.IsEnabled=Login.IsEnabled = false;
            }
        }

        private void btnAcept_Click(object sender, RoutedEventArgs e)
        {
            string connectionStringSQL;

            DbList.Items.Clear();
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
          
            using (SqlConnection connection = new SqlConnection(connectionStringSQL))
            {
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("select Name from sys.sysdatabases", connection);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            DbList.Items.Add(reader["Name"]);
                        }
                    reader.Close();
          
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Win.Close();
        }
    }
}

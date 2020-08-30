using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace TablesAdo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable tbBooks = new DataTable();
        DataTable tbAuthor = new DataTable();
        DataTable tbGenres = new DataTable();
        SqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();

            string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
            
            ReadDb(connection, tbBooks, "select * from Books");
            ReadDb(connection, tbAuthor, "select * from Authors");
            ReadDb(connection, tbGenres, "select * from Genres");
            DgBooks.ItemsSource = tbBooks.DefaultView;
            DgAuthors.ItemsSource = tbAuthor.DefaultView;
            DgGenres.ItemsSource = tbGenres.DefaultView;

        }

        private static void ReadDb(SqlConnection connection, DataTable table, string commandStr)
        {
            SqlCommand command = new SqlCommand(commandStr, connection);
            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        do
                        {
                            int line = 0;
                            while (reader.Read())
                            {
                                if (line == 0)
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        table.Columns.Add(reader.GetName(i));
                                    }
                                    line++;
                                }
                                DataRow row = table.NewRow();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row[i] = reader[i];
                                }
                                table.Rows.Add(row);
                            }
                        } while (reader.NextResult());
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveXML_Click(object sender, RoutedEventArgs e)
        {
            tbBooks.WriteXml("tbBooks.xml");
            tbAuthor.WriteXml("tbAuthor.xml");
            tbGenres.WriteXml("tbGenres.xml");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            if (!String.IsNullOrWhiteSpace(tbId.Text))
            {
                DataRow[] rows = tbBooks.Select($"Id = {int.Parse(tbId.Text)}");
                if (rows.Length > 0)
                    rows[0].Delete();
                SqlCommand command = new SqlCommand($"Delete Books where Id = {tbId.Text}", connection);
                command.ExecuteNonQuery();
                TC.SelectedIndex = 0;
            }
        }
    }
}

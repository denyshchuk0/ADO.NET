using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Windows;

namespace forStudents
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string provider = ConfigurationManager.AppSettings["provider"];

        public static DbProviderFactory factory = DbProviderFactories.GetFactory(provider);
        public DbConnection connection = factory.CreateConnection();
        List<int> StudentsList = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
                connection.Open();
            }
            catch (DbException ex) { MessageBox.Show(ex.Message); }
            CommandDbSelect();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd studentWindow = new StudentAdd();
            studentWindow.btnUpdate.Visibility = Visibility.Hidden;
            ShowNewWindow(studentWindow);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DbCommand command = factory.CreateCommand();
                command.CommandText = $"delete from Student where id = {StudentsList[lbStudents.SelectedIndex]}";
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            lbStudents.Items.Clear();
            StudentsList.Clear();
            CommandDbSelect();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lbStudents.SelectedItem == null)
            {
                MessageBox.Show("Enter Students please)");
            }
            else
            {
                string[] fio = lbStudents.SelectedItem.ToString().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                StudentAdd studentUpWindow = new StudentAdd();
                studentUpWindow.tbName.Text = fio[0].Trim();
                studentUpWindow.tbSurname.Text = fio[1].Trim();
                studentUpWindow.cbGroups.SelectedItem = fio[2];
                studentUpWindow.IdStudents = StudentsList[lbStudents.SelectedIndex];
                ShowNewWindow(studentUpWindow);
            }
        }
        private void Window_Closed(object sender, EventArgs e) => connection.Close();
        private void CommandDbSelect()
        {
            try
            {
                DbCommand command = factory.CreateCommand();
                command.CommandText = "select S.id, S.Name, S.Surname, G.Name from Student S Join Groups G On S.idGroup=G.id";
                command.Connection = connection;

                DbDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        lbStudents.Items.Add($"{reader["Name"]}\t {reader["Surname"]}\t {reader[3]}");
                        StudentsList.Add(Convert.ToInt32(reader["id"]));
                    }
                reader.Close();
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ShowNewWindow(StudentAdd studentUpWindow)
        {
            studentUpWindow.Factory = factory;
            studentUpWindow.Connection = connection;
            studentUpWindow.Owner = this;
            studentUpWindow.Show();
        }
    }
}

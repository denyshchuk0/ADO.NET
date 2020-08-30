using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Windows;

namespace forStudents
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
        SqlConnection connection = new SqlConnection(builder.ConnectionString);
        List<int> StudentsList = new List<int>();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                builder.AsynchronousProcessing = true;
                connection.Open();
            }
            catch (SqlException ex) { MessageBox.Show(ex.Message); }
            CommandDbSelect();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd studentWindow = new StudentAdd();
            studentWindow.btnUpdate.Visibility = Visibility.Hidden;
            studentWindow.cbGroups.SelectedIndex = 0;
            ShowNewWindow(studentWindow);
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = $"delete from Student where id = {StudentsList[lbStudents.SelectedIndex]}";
                command.Connection = connection;
                command.BeginExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally {
                btnReload_Click(null, null);
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
                SqlCommand command = new SqlCommand();
                command.CommandText = "select S.id, S.Name, S.Surname, G.Name from Student S Join Groups G On S.idGroup=G.id";
                command.Connection = connection;

                var reader = command.BeginExecuteReader(ReaderCallBack,command);    
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReaderCallBack(IAsyncResult ar)
        {
            var resault =  (SqlCommand)ar.AsyncState;
            var reader = resault.EndExecuteReader(ar);

            if (reader.HasRows)
                while (reader.Read())
                {
                    Dispatcher.Invoke(() =>
                    {
                        lbStudents.Items.Add($"{reader["Name"]}\t {reader["Surname"]}\t {reader[3]}");
                        StudentsList.Add(Convert.ToInt32(reader["id"]));
                    });
                }
            reader.Close();
        }

        private void ShowNewWindow(StudentAdd studentUpWindow)
        {
            studentUpWindow.Connection = connection;
            studentUpWindow.Owner = this;
            studentUpWindow.Show();
        }
    }
}

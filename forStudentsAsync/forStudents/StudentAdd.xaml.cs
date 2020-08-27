using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using System.Windows;

namespace forStudents
{
    /// <summary>
    /// Логика взаимодействия для StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        public SqlConnection Connection { get; set; }

        List<int> IdGroup = new List<int>();
        public int IdStudents { get; set; }
        public StudentAdd()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = $"insert into Student values('{tbName.Text}','{tbSurname.Text}',{ IdGroup[cbGroups.SelectedIndex]})";
                command.Connection = Connection;
                command.BeginExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "select Name, Id from Groups";
            command.Connection = Connection;
            var reader = command.BeginExecuteReader(ReaderCallBack,command);
        }

        private void ReaderCallBack(IAsyncResult ar)
        {
            var resault = (SqlCommand)ar.AsyncState;
            var reader = resault.EndExecuteReader(ar);
            IdGroup.Clear();
            if (reader.HasRows)
                while (reader.Read())
                {
                    Dispatcher.Invoke(() =>
                    {
                        cbGroups.Items.Add(reader["name"]);
                        IdGroup.Add(Convert.ToInt32(reader["id"]));
                    });
                } 
            reader.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = $"update Student set Name = '{tbName.Text}', Surname = '{tbSurname.Text}', IdGroup = {IdGroup[cbGroups.SelectedIndex]} where id= '{IdStudents}'";
                command.Connection = Connection;
                command.BeginExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

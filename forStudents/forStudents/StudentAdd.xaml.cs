using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows;

namespace forStudents
{
    /// <summary>
    /// Логика взаимодействия для StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        public DbConnection Connection { get; set; }
        public DbProviderFactory Factory { get; set; }

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
                DbCommand command = Factory.CreateCommand();
                command.CommandText = $"insert into Student values('{tbName.Text}','{tbSurname.Text}',{ IdGroup[cbGroups.SelectedIndex]})";
                command.Connection = Connection;
                command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DbCommand command = Factory.CreateCommand();
            command.CommandText = "select Name, Id from Groups";
            command.Connection = Connection;

            DbDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
                while (reader.Read())
                {
                    cbGroups.Items.Add(reader["name"]);
                    IdGroup.Add(Convert.ToInt32(reader["id"]));
                }
            reader.Close();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DbCommand command = Factory.CreateCommand();
                command.CommandText = $"update Student set Name = '{tbName.Text}', Surname = '{tbSurname.Text}', IdGroup = {IdGroup[cbGroups.SelectedIndex]} where id= '{IdStudents}'";
                command.Connection = Connection;
                command.ExecuteNonQuery();
            }
            catch (DbException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

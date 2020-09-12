﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace DataAdapter
{
    public partial class Form1 : Form
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
        SqlConnection connection;
        SqlDataAdapter adapter;
        SqlCommandBuilder builder;
        DataSet set;
        public Form1()
        {
            InitializeComponent();

            connection = new SqlConnection(connectionString);
            adapter = new SqlDataAdapter("SELECT * FROM Books", connection);
            builder = new SqlCommandBuilder(adapter);
            set = new DataSet();
            adapter.Fill(set);

            dataGridView1.DataSource = set.Tables[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] image;
            OpenFileDialog dialog = new OpenFileDialog();
            string fileName = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName.ToString();
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    image = new byte[fs.Length];
                    fs.Read(image, 0, image.Length);
                }
                //enter row
                set.Tables[0].Rows[int.Parse(textBox1.Text)]["Picture"] = image;

                SqlCommand command = new SqlCommand($"Update Books SET Picture = @p1  where Id = @p2");

                command.Parameters.AddWithValue("@p1", image);
                command.Parameters.AddWithValue("@p2", int.Parse(textBox1.Text));

                adapter.UpdateCommand = command;
                adapter.Update(set);
            }
        }
    }
}

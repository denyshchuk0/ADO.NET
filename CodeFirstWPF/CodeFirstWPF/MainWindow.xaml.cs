using CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace CodeFirstWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static AplicationContext context = new AplicationContext();
        List tmp;
        public MainWindow()
        {

            InitializeComponent();
            Title = context.Clients.FirstOrDefault(x => x.Name == "Tom").Name;
            context.SaveChanges();

            dgClients.AutoGenerateColumns = true;
         
            NewMethod(context.Clients);
            context.SaveChanges();

        }

        private void NewMethod(DbSet dbSet)
        {
            foreach (var item in dbSet)
            {
                list.Add(item);
            }

            dgClients.ItemsSource = list;
        }
    }
}

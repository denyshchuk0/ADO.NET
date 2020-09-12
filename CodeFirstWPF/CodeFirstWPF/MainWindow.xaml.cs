using CodeFirst.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CodeFirstWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static AplicationContext context = new AplicationContext();

        List<Client> Clients;
        List<Addres> Addres;
        List<Category> Categories;
        List<Manufacturer> Manufacturers;
        List<Order> Orders;
        List<Product> Products;
        public MainWindow()
        {

            InitializeComponent();
            Title = context.Clients.FirstOrDefault(x => x.Name == "Tom").Name;
            context.SaveChanges();
           
            dgClients.AutoGenerateColumns = true;

            FillGrClients();
            FillGrAddres();
            FillGrCategory();
            FillGrManufacturers();
            FillGrOrders();
            FillGrProduct();

            context.SaveChanges();
        }

        private void FillGrClients()
        {
            Clients = new List<Client>();
            foreach (var item in context.Clients)
            {
                Clients.Add(item);
            }
            dgClients.ItemsSource = Clients;
        }
        private void FillGrProduct()
        {
            Products = new List<Product>();
            foreach (var item in context.Products)
            {
                Products.Add(item);
            }
            dgProducts.ItemsSource = Products;
        }

        private void FillGrAddres()
        {
            Addres = new List<Addres>();
            foreach (var item in context.Addreses)
            {
                Addres.Add(item);
            }
            dgAddres.ItemsSource = Addres;
        }

        private void FillGrCategory()
        {
            Categories = new List<Category>();
            foreach (var item in context.Categories)
            {
                Categories.Add(item);
            }
            dgCategories.ItemsSource = Categories;
        }

        private void FillGrManufacturers()
        {
            Manufacturers = new List<Manufacturer>();
            foreach (var item in context.Manufacturers)
            {
                Manufacturers.Add(item);
            }
            dgManufacturers.ItemsSource = Manufacturers;
        }

        private void FillGrOrders()
        {
            Orders = new List<Order>();
            foreach (var item in context.Orders)
            {
                Orders.Add(item);
            }
            dgOrders.ItemsSource = Orders;
        }
    }
}

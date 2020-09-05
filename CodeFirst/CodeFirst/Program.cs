using CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationContext context = new ApplicationContext();
            #region
            //context.Clients.AddRange(new List<Client>() {
            //    new Client()
            //    {
            //        Name = "Tom"
            //    },
            //     new Client()
            //     {
            //         Name = "Bill"
            //     },
            //    new Client()
            //     {
            //         Name = "Sergey"
            //     },
            //      new Client()
            //      {
            //          Name = "Karrrl"
            //      }});
            //context.SaveChanges();
            #endregion


            //context.Categories.AddRange(new List<Category>() {
            //    new Category {Name = "Shose" },
            //    new Category {Name = "food" },
            //    new Category {Name = "alcohol" },
            //    new Category {Name = "phones" }

            //});

            context.Products.AddRange(new List<Product>() {
            new Product {Name = "IPhone" , Price = 320, Category = context.Categories.FirstOrDefault(x=>x.Name=="phones")},
            new Product {Name = "Meat" , Price = 30, Category = context.Categories.FirstOrDefault(x=>x.Name=="food")},
            new Product {Name = "vodlka" , Price = 320, Category = context.Categories.FirstOrDefault(x=>x.Name=="phones")},
            new Product {Name = "samsung" , Price = 320, Category = context.Categories.FirstOrDefault(x=>x.Name=="alcohol")},
            new Product {Name = "xiaomi" , Price = 320, Category = context.Categories.FirstOrDefault(x=>x.Name=="phones")},
            });
            context.SaveChanges();
            context.Orders.AddRange(new List<Order>()
            {
                new Order {Date = Convert.ToDateTime("20.12.2019"), TotalPrice=3244 , Client = context.Clients.FirstOrDefault(x=>x.Name=="Tom"),
                    Addres = context.Addreses.FirstOrDefault(x=>x.City=="Rovno"), ProductId = context.Products.FirstOrDefault(x=>x.Name == "samsung").Id },
                new Order {Date = Convert.ToDateTime("20.11.2019"), TotalPrice=3244 , Client = context.Clients.FirstOrDefault(x=>x.Name=="Bill"),
                    Addres = context.Addreses.FirstOrDefault(x=>x.City=="Zmerenka"), ProductId = context.Products.FirstOrDefault(x=>x.Name == "vodlka").Id },
                new Order {Date = Convert.ToDateTime("23.12.2019"), TotalPrice=3244 , Client = context.Clients.FirstOrDefault(x=>x.Name=="Tom"),
                    Addres = context.Addreses.FirstOrDefault(x=>x.City=="Vladikavkas"), ProductId = context.Products.FirstOrDefault(x=>x.Name == "vodlka").Id },
                new Order {Date = Convert.ToDateTime("21.12.2019"), TotalPrice=3244 , Client = context.Clients.FirstOrDefault(x=>x.Name=="Karrrl"),
                    Addres = context.Addreses.FirstOrDefault(x=>x.City=="Tokio"), ProductId = context.Products.FirstOrDefault(x=>x.Name == "vodlka").Id },
            });
            context.SaveChanges();

        }
    }
}

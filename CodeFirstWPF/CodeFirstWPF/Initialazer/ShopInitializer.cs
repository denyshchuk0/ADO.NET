﻿using CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstWPF.Initialazer
{
    public class ShopInitializer : DropCreateDatabaseAlways<AplicationContext>
    {
        protected override void Seed(AplicationContext context)
        {
            context.Clients.AddRange(new List<Client>() {
                new Client() { Name = "Tom"},
                new Client() { Name = "Bill"},
                new Client() { Name = "Sergey"},
                new Client() { Name = "Karrrl"}
            });

            context.Categories.AddRange(new List<Category>() {
                new Category() { Name = "Shoses"},
                new Category() { Name = "Foods"},
                new Category() { Name = "Phones"},
                new Category() { Name = "Books"},
            });

            context.Addreses.AddRange(new List<Addres>() {
                new Addres(){Country = "Ukraine", City = "Rovno", Street = "Soborna",NumOfBuild= 10 },
                new Addres(){Country = "Japan", City = "Tokio", Street = "FGEHDKS",NumOfBuild= 2 },
                new Addres(){Country = "USA", City = "LA", Street = "Grow",NumOfBuild= 1 },
                new Addres(){Country = "Ukraine", City = "Zmerenka", Street = "Tarasa Shewchenka",NumOfBuild= 404},
            });

            context.Manufacturers.AddRange(new List<Manufacturer>() { 
                new Manufacturer() { Name = "OOO Adidas", Phone = "098473463"},
                new Manufacturer() { Name = "OOO Apple", Phone = "098473623"},
                new Manufacturer() { Name = "OOO BookmanC",Phone ="098735243"},
                new Manufacturer() { Name = "OOO Fozzy", Phone = "098352637"}
            });
            context.Manufacturers.FirstOrDefault(x => x.Name == "OOO Adidas").Addreses.Add(context.Addreses.FirstOrDefault(x => x.Country.Equals("Ukraine")));
            context.Manufacturers.FirstOrDefault(x => x.Name == "OOO Apple").Addreses.Add(context.Addreses.FirstOrDefault(x => x.Country.Equals("Japan")));
            context.Manufacturers.FirstOrDefault(x => x.Name == "OOO BookmanC").Addreses.Add(context.Addreses.FirstOrDefault(x => x.Country.Equals("USA")));
            context.Manufacturers.FirstOrDefault(x => x.Name == "OOO Fozzy").Addreses.Add(context.Addreses.FirstOrDefault(x => x.City.Equals("Zmerenka")));

            context.Orders.AddRange(new List<Order>() { 
                new Order() {Date = Convert.ToDateTime("20.08.2002"), TotalPrice = 2234, Client=context.Clients.FirstOrDefault(x=>x.Name.Equals("Bill")),  }
            });




            base.Seed(context);
        }
    }
}

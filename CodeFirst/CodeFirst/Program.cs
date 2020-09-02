using CodeFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
            context.Manufacturers.AddRange(new List<Manufacturer>()
            {
                new Manufacturer { Name = "Adidas",Phone="09856473387", Addreses=context.Addreses.FirstOrDefault(x=>x.Country.Equals("Japan")) }


            }) ;
            context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
     public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double TotalPrice { get; set; }
        public int ClientId { get; set; }
        public int AdressId { get; set; }
        public virtual Client Client { get; set; } 
        public virtual Addres Addres{ get; set; } 
        public virtual ICollection<Product> Products { get; set; }

        public Order() 
        {
            Products = new List<Product>();
        }
    }
}

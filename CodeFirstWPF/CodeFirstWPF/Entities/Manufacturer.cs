using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Addres> Addreses { get; set; }

        public Manufacturer()
        {
            Addreses = new List<Addres>();
        }
    }
}

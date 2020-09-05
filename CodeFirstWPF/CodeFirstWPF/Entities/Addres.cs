using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Entities
{
    public class Addres
    {
        [Key]
        public int Id { get; set; }
        [Required] [MaxLength(200)]
        public string Country { get; set; }
        [Required]
        [MaxLength(200)]
        public string City { get; set; }
        [Required]
        [MaxLength(200)]
        public string Street { get; set; }
        [Required]
        public int NumOfBuild { get; set; }
        public virtual Manufacturer Manufacturers { get; set; }

    }
}

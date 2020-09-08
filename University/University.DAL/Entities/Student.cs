namespace University.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(30)]
        public string Surname { get; set; }

        public int? IdGroup { get; set; }

        public virtual Groups Groups { get; set; }
    }
}

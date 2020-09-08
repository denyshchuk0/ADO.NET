namespace University.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AplicationContext : DbContext
    {
        public AplicationContext()
            : base("name=AplicationContext")
        {
        }

        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Student> Student { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Groups>()
                .HasMany(e => e.Student)
                .WithOptional(e => e.Groups)
                .HasForeignKey(e => e.IdGroup)
                .WillCascadeOnDelete();
        }
    }
}

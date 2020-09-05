namespace CodeFirstWPF
{
    using CodeFirst.Entities;
    using CodeFirstWPF.Initialazer;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AplicationContext : DbContext
    {
        // Контекст настроен для использования строки подключения "AplicationContext" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "CodeFirstWPF.AplicationContext" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "AplicationContext" 
        // в файле конфигурации приложения.
        public AplicationContext()
            : base("name=AplicationContext")
        {
            Database.SetInitializer(new ShopInitializer());
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Addres> Addreses { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}
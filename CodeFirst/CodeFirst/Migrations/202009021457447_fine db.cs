namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finedb : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Addres", name: "Manufacturer_Id", newName: "Manufacturers_Id");
            RenameIndex(table: "dbo.Addres", name: "IX_Manufacturer_Id", newName: "IX_Manufacturers_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Addres", name: "IX_Manufacturers_Id", newName: "IX_Manufacturer_Id");
            RenameColumn(table: "dbo.Addres", name: "Manufacturers_Id", newName: "Manufacturer_Id");
        }
    }
}

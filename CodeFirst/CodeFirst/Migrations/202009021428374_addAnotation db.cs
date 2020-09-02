namespace CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAnotationdb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addres", "Country", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Addres", "City", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Addres", "Street", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Addres", "NumOfBuild", c => c.Int(nullable: false));
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Products", "Price", c => c.Double());
            AlterColumn("dbo.Manufacturers", "Name", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Clients", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Name", c => c.String());
            AlterColumn("dbo.Manufacturers", "Name", c => c.String());
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Addres", "NumOfBuild", c => c.String());
            AlterColumn("dbo.Addres", "Street", c => c.String());
            AlterColumn("dbo.Addres", "City", c => c.String());
            AlterColumn("dbo.Addres", "Country", c => c.String());
        }
    }
}

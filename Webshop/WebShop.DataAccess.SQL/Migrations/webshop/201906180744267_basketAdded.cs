namespace WebShop.DataAccess.SQL.Migrations.webshop
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class basketAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BasketItems",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        BasketID = c.String(maxLength: 128),
                        ProductID = c.String(),
                        Quantity = c.Int(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Baskets", t => t.BasketID)
                .Index(t => t.BasketID);
            
            CreateTable(
                "dbo.Baskets",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BasketItems", "BasketID", "dbo.Baskets");
            DropIndex("dbo.BasketItems", new[] { "BasketID" });
            DropTable("dbo.Baskets");
            DropTable("dbo.BasketItems");
        }
    }
}

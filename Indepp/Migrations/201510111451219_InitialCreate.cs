namespace Indepp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        PlaceID = c.Int(nullable: false),
                        Address1 = c.String(maxLength: 50),
                        Address2 = c.String(maxLength: 50),
                        Address3 = c.String(maxLength: 50),
                        City = c.String(nullable: false, maxLength: 50),
                        County = c.String(maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 50),
                        PostCode = c.String(maxLength: 32),
                        Latitude = c.Decimal(precision: 18, scale: 9),
                        Longitude = c.Decimal(precision: 18, scale: 9),
                    })
                .PrimaryKey(t => t.PlaceID)
                .ForeignKey("dbo.Place", t => t.PlaceID)
                .Index(t => t.PlaceID);
            
            CreateTable(
                "dbo.Place",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Category = c.String(nullable: false, maxLength: 50),
                        Website = c.String(),
                        Telephone = c.String(),
                        Email = c.String(),
                        UserName = c.String(maxLength: 50),
                        UserEmail = c.String(),
                        UserContributed = c.Boolean(nullable: false),
                        Reviewed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        ShortDescription = c.String(),
                        Description = c.String(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        ModifiedOn = c.DateTime(),
                        PlaceID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Place", t => t.PlaceID)
                .Index(t => t.PlaceID);
            
            CreateTable(
                "dbo.BlogPost",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        ShortDescription = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        PlaceID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Place", t => t.PlaceID)
                .Index(t => t.PlaceID);
            
            CreateTable(
                "dbo.PlaceDescription",
                c => new
                    {
                        PlaceID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.PlaceID)
                .ForeignKey("dbo.Place", t => t.PlaceID)
                .Index(t => t.PlaceID);
            
            CreateTable(
                "dbo.WorkingHour",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PlaceID = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        OpenTime = c.Time(precision: 7),
                        CloseTime = c.Time(precision: 7),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Place", t => t.PlaceID, cascadeDelete: true)
                .Index(t => t.PlaceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "PlaceID", "dbo.Place");
            DropForeignKey("dbo.WorkingHour", "PlaceID", "dbo.Place");
            DropForeignKey("dbo.PlaceDescription", "PlaceID", "dbo.Place");
            DropForeignKey("dbo.BlogPost", "PlaceID", "dbo.Place");
            DropForeignKey("dbo.Article", "PlaceID", "dbo.Place");
            DropIndex("dbo.WorkingHour", new[] { "PlaceID" });
            DropIndex("dbo.PlaceDescription", new[] { "PlaceID" });
            DropIndex("dbo.BlogPost", new[] { "PlaceID" });
            DropIndex("dbo.Article", new[] { "PlaceID" });
            DropIndex("dbo.Address", new[] { "PlaceID" });
            DropTable("dbo.WorkingHour");
            DropTable("dbo.PlaceDescription");
            DropTable("dbo.BlogPost");
            DropTable("dbo.Article");
            DropTable("dbo.Place");
            DropTable("dbo.Address");
        }
    }
}

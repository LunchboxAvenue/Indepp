namespace Indepp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GooglePlaceId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Place", "GooglePlaceId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Place", "GooglePlaceId");
        }
    }
}

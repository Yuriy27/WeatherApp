namespace WheatherForecast.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CityEntities", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CityEntities", "Name", c => c.String());
        }
    }
}

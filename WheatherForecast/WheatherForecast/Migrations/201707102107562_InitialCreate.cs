namespace WheatherForecast.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CityEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForecastEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Date = c.DateTime(nullable: false),
                        Pressure = c.Double(nullable: false),
                        Humidity = c.Double(nullable: false),
                        TemperatureMorning = c.Double(nullable: false),
                        TemperatureDay = c.Double(nullable: false),
                        TemperatureEvening = c.Double(nullable: false),
                        TemperatureNight = c.Double(nullable: false),
                        WindSpeed = c.Double(nullable: false),
                        Description = c.String(),
                        Icon = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ForecastEntities");
            DropTable("dbo.CityEntities");
        }
    }
}

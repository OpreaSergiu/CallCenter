namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkPlatformModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientReference = c.String(),
                        ClientID = c.String(),
                        Name = c.String(),
                        AssignAmount = c.Single(nullable: false),
                        TotalReceived = c.Single(nullable: false),
                        OtherDue = c.Single(nullable: false),
                        TotalDue = c.Single(nullable: false),
                        Desk = c.String(),
                        Status = c.String(),
                        PalacementDate = c.DateTime(nullable: false),
                        LastWorkDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.WorkPlatformModels");
        }
    }
}

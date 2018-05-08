namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl17 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoginLogsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.Int(nullable: false),
                        UserRole = c.Int(nullable: false),
                        LoginDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoginLogsModels");
        }
    }
}

namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl18 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LoginLogsModels", "UserEmail", c => c.String());
            AlterColumn("dbo.LoginLogsModels", "UserRole", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LoginLogsModels", "UserRole", c => c.Int(nullable: false));
            AlterColumn("dbo.LoginLogsModels", "UserEmail", c => c.Int(nullable: false));
        }
    }
}

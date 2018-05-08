namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentsModels", "PostedFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.PaymentsModels", "PosteFlag");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PaymentsModels", "PosteFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.PaymentsModels", "PostedFlag");
        }
    }
}

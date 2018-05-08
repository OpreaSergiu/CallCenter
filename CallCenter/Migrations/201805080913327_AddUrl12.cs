namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentsModels", "PosteFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentsModels", "PosteFlag");
        }
    }
}

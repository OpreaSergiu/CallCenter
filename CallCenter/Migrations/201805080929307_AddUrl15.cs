namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceModels", "PostedFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceModels", "PostedFlag");
        }
    }
}

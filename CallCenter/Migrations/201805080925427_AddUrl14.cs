namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceModels", "PaymentRequestFlag", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.InvoiceModels", "PaymentRequestFlag");
        }
    }
}

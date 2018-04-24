namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AddressModels", "AccountNumber", c => c.Int(nullable: false));
            AddColumn("dbo.InvoiceModels", "AccountNumber", c => c.Int(nullable: false));
            AddColumn("dbo.NotesModels", "AccountNumber", c => c.Int(nullable: false));
            AddColumn("dbo.PhoneModels", "AccountNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PhoneModels", "AccountNumber");
            DropColumn("dbo.NotesModels", "AccountNumber");
            DropColumn("dbo.InvoiceModels", "AccountNumber");
            DropColumn("dbo.AddressModels", "AccountNumber");
        }
    }
}

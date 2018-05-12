namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        FullName = c.String(),
                        Contact = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Country = c.String(),
                        TimeZone = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.InvoiceModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        Invoice = c.String(),
                        Status = c.String(),
                        Amount = c.Single(nullable: false),
                        Due = c.Single(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        PaymentRequestFlag = c.Boolean(nullable: false),
                        PostedFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LoginLogsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        UserRole = c.String(),
                        LoginDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        SeqNumber = c.Int(nullable: false),
                        UserCode = c.String(),
                        ActionCode = c.String(),
                        Status = c.String(),
                        Desk = c.String(),
                        Note = c.String(),
                        NoteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaymentsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        ClientID = c.String(),
                        ClientReference = c.String(),
                        Invoice = c.String(),
                        Amount = c.String(),
                        PaymentDate = c.DateTime(),
                        Approve = c.Boolean(nullable: false),
                        PostedFlag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountNumber = c.Int(nullable: false),
                        Prefix = c.String(),
                        PhoneNumber = c.String(),
                        Extension = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserDeskModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        Desk = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropTable("dbo.UserDeskModels");
            DropTable("dbo.PhoneModels");
            DropTable("dbo.PaymentsModels");
            DropTable("dbo.NotesModels");
            DropTable("dbo.LoginLogsModels");
            DropTable("dbo.InvoiceModels");
            DropTable("dbo.AddressModels");
        }
    }
}

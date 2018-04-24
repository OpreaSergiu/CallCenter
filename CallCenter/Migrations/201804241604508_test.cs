namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AddressModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
                        Invoice = c.String(),
                        Status = c.String(),
                        Amount = c.Single(nullable: false),
                        Due = c.Single(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NotesModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(),
                        Action = c.String(),
                        Status = c.String(),
                        Desk = c.String(),
                        Note = c.String(),
                        NoteDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhoneModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prefix = c.String(),
                        PhoneNumber = c.String(),
                        Extension = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PhoneModels");
            DropTable("dbo.NotesModels");
            DropTable("dbo.InvoiceModels");
            DropTable("dbo.AddressModels");
        }
    }
}

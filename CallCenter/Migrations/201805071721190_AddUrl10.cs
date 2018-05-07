namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl10 : DbMigration
    {
        public override void Up()
        {
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PaymentsModels");
        }
    }
}

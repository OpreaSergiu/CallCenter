namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PaymentsModels", "Amount", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PaymentsModels", "Amount", c => c.String());
        }
    }
}

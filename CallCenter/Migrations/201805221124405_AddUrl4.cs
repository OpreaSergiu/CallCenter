namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SurveyModels", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SurveyModels", "Rating", c => c.String());
        }
    }
}

namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserEmail = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        Suggestions = c.String(),
                        Rating = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SurveyModels");
        }
    }
}

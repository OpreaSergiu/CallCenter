namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotesModels", "UserCode", c => c.String());
            DropColumn("dbo.NotesModels", "User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotesModels", "User", c => c.String());
            DropColumn("dbo.NotesModels", "UserCode");
        }
    }
}

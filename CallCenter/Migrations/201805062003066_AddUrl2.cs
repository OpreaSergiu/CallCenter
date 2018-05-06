namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotesModels", "ActionCode", c => c.String());
            DropColumn("dbo.NotesModels", "Action");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotesModels", "Action", c => c.String());
            DropColumn("dbo.NotesModels", "ActionCode");
        }
    }
}

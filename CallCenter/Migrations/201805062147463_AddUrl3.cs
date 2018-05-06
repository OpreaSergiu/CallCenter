namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUrl3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.NotesModels", "NoteDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.NotesModels", "NoteDate", c => c.DateTime(nullable: false));
        }
    }
}

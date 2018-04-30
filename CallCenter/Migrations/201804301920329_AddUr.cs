namespace CallCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NotesModels", "SeqNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotesModels", "SeqNumber");
        }
    }
}

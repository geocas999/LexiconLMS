namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addeddeadlineproptodocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Deadline", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "Deadline");
        }
    }
}

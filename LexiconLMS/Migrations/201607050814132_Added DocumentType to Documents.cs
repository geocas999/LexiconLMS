namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDocumentTypetoDocuments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "DocumentType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documents", "DocumentType");
        }
    }
}

namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeddatatypeforDocumentTypefromstringtoDocumentTypeinDocumentModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documents", "DocumentType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documents", "DocumentType", c => c.String());
        }
    }
}

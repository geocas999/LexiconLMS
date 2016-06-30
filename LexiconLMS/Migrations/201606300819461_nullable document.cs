namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullabledocument : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        TimeStamp = c.DateTime(nullable: false),
                        Uploader = c.DateTime(nullable: false),
                        CourseId = c.Int(),
                        ModuleId = c.Int(),
                        ActivityId = c.Int(),
                    })
                .PrimaryKey(t => t.DocumentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Documents");
        }
    }
}

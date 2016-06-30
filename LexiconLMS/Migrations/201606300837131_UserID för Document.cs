namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIDförDocument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Documents", "UserId");
            AddForeignKey("dbo.Documents", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Documents", "Uploader");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Documents", "Uploader", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Documents", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Documents", new[] { "UserId" });
            DropColumn("dbo.Documents", "UserId");
        }
    }
}

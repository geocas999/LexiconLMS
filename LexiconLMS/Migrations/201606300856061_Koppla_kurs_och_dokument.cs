namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Koppla_kurs_och_dokument : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Documents", "CourseId");
            AddForeignKey("dbo.Documents", "CourseId", "dbo.Courses", "CourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "CourseId", "dbo.Courses");
            DropIndex("dbo.Documents", new[] { "CourseId" });
        }
    }
}

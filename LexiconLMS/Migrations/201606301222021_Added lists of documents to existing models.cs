namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedlistsofdocumentstoexistingmodels : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Documents", "CourseId");
            CreateIndex("dbo.Documents", "ModuleId");
            AddForeignKey("dbo.Documents", "CourseId", "dbo.Courses", "CourseId");
            AddForeignKey("dbo.Documents", "ModuleId", "dbo.Modules", "ModuleId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documents", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Documents", "CourseId", "dbo.Courses");
            DropIndex("dbo.Documents", new[] { "ModuleId" });
            DropIndex("dbo.Documents", new[] { "CourseId" });
        }
    }
}

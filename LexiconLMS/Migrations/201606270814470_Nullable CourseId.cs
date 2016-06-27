namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableCourseId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "CourseId" });
            AlterColumn("dbo.AspNetUsers", "CourseId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "CourseId");
            AddForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses", "CourseId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "CourseId" });
            AlterColumn("dbo.AspNetUsers", "CourseId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "CourseId");
            AddForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
        }
    }
}

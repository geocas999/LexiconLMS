namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedActivitymodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        ModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.ModuleId);
            
            CreateIndex("dbo.Documents", "ActivityId");
            AddForeignKey("dbo.Documents", "ActivityId", "dbo.Activities", "ActivityId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropForeignKey("dbo.Documents", "ActivityId", "dbo.Activities");
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            DropIndex("dbo.Documents", new[] { "ActivityId" });
            DropTable("dbo.Activities");
        }
    }
}

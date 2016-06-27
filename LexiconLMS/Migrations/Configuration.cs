namespace LexiconLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LexiconLMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LexiconLMS.Models.ApplicationDbContext context)
        {
            var uStore = new UserStore<ApplicationUser>(context);
            var uManager = new UserManager<ApplicationUser>(uStore);
            var rStore = new RoleStore<IdentityRole>(context);
            var rManager = new RoleManager<IdentityRole>(rStore);

            foreach (var userName in new[] { "student@lexicon.se", "teacher@lexicon.se" })
            {
                var user = new ApplicationUser { UserName = userName };
                uManager.Create(user, "pass123");
            }

            foreach (var roleName in new[] { "Teacher", "Student" })
            {
                var role = new IdentityRole { Name = roleName };
                rManager.Create(role);
            }

            var student = uManager.FindByName("student@lexicon.se");
            var teacher = uManager.FindByName("teacher@lexicon.se");

            uManager.AddToRole(student.Id, "Student");
            uManager.AddToRole(teacher.Id, "Teacher");
        }
    }
}

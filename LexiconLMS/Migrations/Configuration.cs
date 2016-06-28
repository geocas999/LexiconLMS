namespace LexiconLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
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
            var list = new List<ApplicationUser>();

            foreach (var userName in new[] { "student@lexicon.se", "teacher@lexicon.se" })
            {
                var user = new ApplicationUser { UserName = userName };
                list.Add(user);
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



            var course = new Course {CourseId = 1, Name = ".NET H�st 2016", Description = "Blabla", StartDate = DateTime.Now, Students = list};

            var module = new Module { ModuleId = 1, Name = "MVC5", Description = "Blabla", StartDate = DateTime.Now.AddDays(7),
                                EndDate = DateTime.Now.AddDays(14), CourseId = course.CourseId};

            student.CourseId = course.CourseId;
            teacher.CourseId = course.CourseId;

            context.Modules.AddOrUpdate(module);
            context.Courses.AddOrUpdate(course);
            context.SaveChanges();
        }
    }
}

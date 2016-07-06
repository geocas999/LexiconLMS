namespace LexiconLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var uStore = new UserStore<ApplicationUser>(context);
            var uManager = new UserManager<ApplicationUser>(uStore);
            var rStore = new RoleStore<IdentityRole>(context);
            var rManager = new RoleManager<IdentityRole>(rStore);
            var list = new List<ApplicationUser>();

            var course = new Course { CourseId = 1, Name = ".NET Vår 2016", Description = "Påbyggnadsutbildning", StartDate = DateTime.Now, Students = list };
            var course2 = new Course { CourseId = 2, Name = ".NET Höst 2016", Description = "Påbyggnadsutbildning", StartDate = DateTime.Now };
            var course3 = new Course { CourseId = 3, Name = "Java Vår 2016", Description = "Påbyggnadsutbildning", StartDate = DateTime.Now };
            var course4 = new Course { CourseId = 4, Name = "Java Höst 2016", Description = "Påbyggnadsutbildning", StartDate = DateTime.Now };
            var course5 = new Course { CourseId = 5, Name = "Sharepoint Vår 2016", Description = "IT-support med Sharepoint", StartDate = DateTime.Now };
            var course6 = new Course { CourseId = 6, Name = "Sharepoint Höst 2016", Description = "IT-support med Sharepoint", StartDate = DateTime.Now };

            var module = new Module
            {
                ModuleId = 1,
                Name = "C#",
                Description = "Introduktionskurs till C#",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                CourseId = course.CourseId
            };

            var module2 = new Module
            {
                ModuleId = 2,
                Name = "Webb",
                Description = "HTML, CSS, Bootstrap, Javascript",
                StartDate = DateTime.Now.AddDays(14),
                EndDate = DateTime.Now.AddDays(21),
                CourseId = course.CourseId
            };

            var module3 = new Module
            {
                ModuleId = 3,
                Name = "Git",
                Description = "Git med Github",
                StartDate = DateTime.Now.AddDays(21),
                EndDate = DateTime.Now.AddDays(22),
                CourseId = course.CourseId
            };

            var module4 = new Module
            {
                ModuleId = 4,
                Name = "MVC",
                Description = "ASP.NET MVC, SQL, EntityFramework, AngularJS",
                StartDate = DateTime.Now.AddDays(22),
                EndDate = DateTime.Now.AddDays(35),
                CourseId = course.CourseId
            };

            var module5 = new Module
            {
                ModuleId = 5,
                Name = "Testning",
                Description = "ISTQB, TDD",
                StartDate = DateTime.Now.AddDays(35),
                EndDate = DateTime.Now.AddDays(42),
                CourseId = course.CourseId
            };

            var module6 = new Module
            {
                ModuleId = 6,
                Name = "Projektmetoder",
                Description = "Scrum och andra projektmetoder",
                StartDate = DateTime.Now.AddDays(42),
                EndDate = DateTime.Now.AddDays(44),
                CourseId = course.CourseId
            };

            var module7 = new Module
            {
                ModuleId = 7,
                Name = "Projekt",
                Description = "Projekt på 4 veckor.",
                StartDate = DateTime.Now.AddDays(44),
                EndDate = DateTime.Now.AddDays(72),
                CourseId = course.CourseId
            };

            var activity = new Activity() { ActivityId = 1, Description = "Pluralsight C# Fundamentals, del 1", Type = ActivityType.Elearning, StartTime = DateTime.Now,
                                            EndTime = DateTime.Now.AddDays(1), ModuleId = module.ModuleId };
            var activity2 = new Activity() { ActivityId = 2, Description = "Pluralsight C# Fundamentals, del 2", Type = ActivityType.Elearning, StartTime = DateTime.Now,
                                            EndTime = DateTime.Now.AddDays(1), ModuleId = module.ModuleId };
            var activity3 = new Activity() { ActivityId = 3, Description = "C# lecture with Adrian", Type = ActivityType.Lecture, StartTime = DateTime.Now,
                                            EndTime = DateTime.Now.AddDays(1), ModuleId = module.ModuleId };
            var activity4 = new Activity() { ActivityId = 4, Description = "Exercise 1 - C#", Type = ActivityType.Exercise, StartTime = DateTime.Now,
                                            EndTime = DateTime.Now.AddDays(1), ModuleId = module.ModuleId };

            context.Modules.AddOrUpdate(module);
            context.Modules.AddOrUpdate(module2);
            context.Modules.AddOrUpdate(module3);
            context.Modules.AddOrUpdate(module4);
            context.Modules.AddOrUpdate(module5);
            context.Modules.AddOrUpdate(module6);
            context.Modules.AddOrUpdate(module7);

            context.Courses.AddOrUpdate(course);
            context.Courses.AddOrUpdate(course2);
            context.Courses.AddOrUpdate(course3);
            context.Courses.AddOrUpdate(course4);
            context.Courses.AddOrUpdate(course5);
            context.Courses.AddOrUpdate(course6);

            context.Activities.AddOrUpdate(activity);
            context.Activities.AddOrUpdate(activity2);
            context.Activities.AddOrUpdate(activity3);
            context.Activities.AddOrUpdate(activity4);

            context.SaveChanges();

            foreach (var roleName in new[] { "Teacher", "Student" })
            {
                var role = new IdentityRole { Name = roleName };
                rManager.Create(role);
            }

            var users = new List<ApplicationUser>();

            var user1 = new ApplicationUser { UserName = "student@lexicon.se", Email = "student@lexicon.se", Name = "Student Studentsson", PhoneNumber = "070-0011223", CourseId = 1 };
            var user2 = new ApplicationUser { UserName = "teacher@lexicon.se", Email = "teacher@lexicon.se", Name = "Teacher Teacher", PhoneNumber = "073-555138232", CourseId = 3 };
            var user3 = new ApplicationUser { UserName = "fredrik@lexicon.se", Email = "fredrik@lexicon.se", Name = "Fredrik Fredriksson", PhoneNumber = "08-2501223", CourseId = 2 };
            var user4 = new ApplicationUser { UserName = "anette@lexicon.se", Email = "anette@lexicon.se", Name = "Anette Andersson", PhoneNumber = "076-7788999", CourseId = 3 };
            var user5 = new ApplicationUser { UserName = "yaser@lexicon.se", Email = "yaser@lexicon.se", Name = "Yaser Yasersson", PhoneNumber = "070-0011223", CourseId = 1 };
            var user6 = new ApplicationUser { UserName = "george@lexicon.se", Email = "george@lexicon.se", Name = "George Caspersson", PhoneNumber = "070-0011223", CourseId = 1 };

            users.Add(user1);
            users.Add(user2);
            users.Add(user3);
            users.Add(user4);
            users.Add(user5);
            users.Add(user6);

            foreach (var userName in users)
            {
                var user = userName;
                uManager.Create(user, "pass123");
                context.Users.AddOrUpdate(u => u.UserName, user);

                if (user.UserName == "teacher@lexicon.se")
                {
                    user = uManager.FindByName(user.UserName);
                    uManager.AddToRole(user.Id, "Teacher");
                    var document = new Document()
                    {
                        DocumentId = 1,
                        CourseId = user.CourseId,
                        Description = "Schedule",
                        Name = "Schedule",
                        TimeStamp = DateTime.Now,
                        Type = ".pdf",
                        UserId = user.Id
                    };
                    context.Documents.AddOrUpdate(d => d.DocumentId, document);
                }
                else
                {
                    uManager.AddToRole(uManager.FindByName(user.UserName).Id, "Student");
                    user.CourseId = course.CourseId;
                }
            }
        }
    }
}

namespace LexiconLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
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

            SeedCourses(context);

            SeedModules(context);

            SeedActivities(context);

            context.SaveChanges();

            SeedRoles(rManager);

            var users = SeedUsers();

            var tempUser = new ApplicationUser();

            foreach (var user in users)
            {
                uManager.Create(user, "pass123");
                context.Users.AddOrUpdate(u => u.UserName, user);

                if (user.UserName == "teacher@lexicon.se" || user.UserName == "george@lexicon.se" || user.UserName == "anette@lexicon.se" || user.UserName == "yaser@lexicon.se" || user.UserName == "fredrik@lexicon.se")
                {
                    tempUser = uManager.FindByName(user.UserName);
                    uManager.AddToRole(tempUser.Id, "Teacher");
                }
                else
                {
                    uManager.AddToRole(uManager.FindByName(user.UserName).Id, "Student");
                    user.CourseId = 1;
                }


            }

            SeedDocuments(context, tempUser);
        }

        private static void SeedCourses(ApplicationDbContext context)
        {
            var course1 = new Course
            {
                CourseId = 1,
                Name = ".NET Vår 2016",
                Description = "Påbyggnadsutbildning",
                StartDate = DateTime.Now,
                Students = new List<ApplicationUser>()
            };
            var course2 = new Course
            {
                CourseId = 2,
                Name = ".NET Höst 2016",
                Description = "Påbyggnadsutbildning",
                StartDate = DateTime.Now
            };
            var course3 = new Course
            {
                CourseId = 3,
                Name = "Java Vår 2016",
                Description = "Påbyggnadsutbildning",
                StartDate = DateTime.Now
            };
            var course4 = new Course
            {
                CourseId = 4,
                Name = "Java Höst 2016",
                Description = "Påbyggnadsutbildning",
                StartDate = DateTime.Now
            };
            var course5 = new Course
            {
                CourseId = 5,
                Name = "Sharepoint Vår 2016",
                Description = "IT-support med Sharepoint",
                StartDate = DateTime.Now
            };
            var course6 = new Course
            {
                CourseId = 6,
                Name = "Sharepoint Höst 2016",
                Description = "IT-support med Sharepoint",
                StartDate = DateTime.Now
            };

            context.Courses.AddOrUpdate(course1);
            context.Courses.AddOrUpdate(course2);
            context.Courses.AddOrUpdate(course3);
            context.Courses.AddOrUpdate(course4);
            context.Courses.AddOrUpdate(course5);
            context.Courses.AddOrUpdate(course6);
        }

        private static void SeedModules(ApplicationDbContext context)
        {
            var module = new Module
            {
                ModuleId = 1,
                Name = "C#",
                Description = "Introduktionskurs till C#",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                CourseId = 1
            };

            var module2 = new Module
            {
                ModuleId = 2,
                Name = "Webb",
                Description = "HTML, CSS, Bootstrap, Javascript",
                StartDate = DateTime.Now.AddDays(14),
                EndDate = DateTime.Now.AddDays(21),
                CourseId = 1
            };

            var module3 = new Module
            {
                ModuleId = 3,
                Name = "Git",
                Description = "Git med Github",
                StartDate = DateTime.Now.AddDays(21),
                EndDate = DateTime.Now.AddDays(22),
                CourseId = 1
            };

            var module4 = new Module
            {
                ModuleId = 4,
                Name = "MVC",
                Description = "ASP.NET MVC, SQL, EntityFramework, AngularJS",
                StartDate = DateTime.Now.AddDays(22),
                EndDate = DateTime.Now.AddDays(35),
                CourseId = 1
            };

            var module5 = new Module
            {
                ModuleId = 5,
                Name = "Testning",
                Description = "ISTQB, TDD",
                StartDate = DateTime.Now.AddDays(35),
                EndDate = DateTime.Now.AddDays(42),
                CourseId = 1
            };

            var module6 = new Module
            {
                ModuleId = 6,
                Name = "Projektmetoder",
                Description = "Scrum och andra projektmetoder",
                StartDate = DateTime.Now.AddDays(42),
                EndDate = DateTime.Now.AddDays(44),
                CourseId = 1
            };

            var module7 = new Module
            {
                ModuleId = 7,
                Name = "Projekt",
                Description = "Projekt på 4 veckor.",
                StartDate = DateTime.Now.AddDays(44),
                EndDate = DateTime.Now.AddDays(72),
                CourseId = 1
            };

            context.Modules.AddOrUpdate(module);
            context.Modules.AddOrUpdate(module2);
            context.Modules.AddOrUpdate(module3);
            context.Modules.AddOrUpdate(module4);
            context.Modules.AddOrUpdate(module5);
            context.Modules.AddOrUpdate(module6);
            context.Modules.AddOrUpdate(module7);
        }

        private static void SeedActivities(ApplicationDbContext context)
        {
            var activity = new Activity()
            {
                ActivityId = 1,
                Description = "Pluralsight C# Fundamentals, del 1",
                Type = ActivityType.Elearning,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                ModuleId = 1
            };
            var activity2 = new Activity()
            {
                ActivityId = 2,
                Description = "Pluralsight C# Fundamentals, del 2",
                Type = ActivityType.Elearning,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                ModuleId = 1
            };
            var activity3 = new Activity()
            {
                ActivityId = 3,
                Description = "C# lecture with Adrian",
                Type = ActivityType.Lecture,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                ModuleId = 1
            };
            var activity4 = new Activity()
            {
                ActivityId = 4,
                Description = "Exercises - C#",
                Type = ActivityType.Exercise,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddDays(1),
                ModuleId = 1
            };

            context.Activities.AddOrUpdate(activity);
            context.Activities.AddOrUpdate(activity2);
            context.Activities.AddOrUpdate(activity3);
            context.Activities.AddOrUpdate(activity4);
        }

        private static void SeedRoles(RoleManager<IdentityRole> rManager)
        {
            foreach (var roleName in new[] { "Teacher", "Student" })
            {
                var role = new IdentityRole { Name = roleName };
                rManager.Create(role);
            }
        }

        //2016-07-07 Changed this SeedUsers to List
        private static List<ApplicationUser> SeedUsers()
        {
            var users = new List<ApplicationUser>
            {
            new ApplicationUser{
                UserName = "student@lexicon.se",
                Email = "student@lexicon.se",
                Name = "Student Studentsson",
                PhoneNumber = "070-0011223",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "teacher@lexicon.se",
                Email = "teacher@lexicon.se",
                Name = "Teacher Teachersson",
                PhoneNumber = "073-555138232",
                //CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "fredrik@lexicon.se",
                Email = "fredrik@lexicon.se",
                Name = "Fredrik Lindroth",
                PhoneNumber = "08-2501223",
                //CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "anette@lexicon.se",
                Email = "anette@lexicon.se",
                Name = "Anette Tillbom",
                PhoneNumber = "076-7788999",
                //CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "yaser@lexicon.se",
                Email = "yaser@lexicon.se",
                Name = "Yaser Mosavi",
                PhoneNumber = "070-0011223",
                //CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "george@lexicon.se",
                Email = "george@lexicon.se",
                Name = "George Caspersson",
                PhoneNumber = "070-0011223",
                //CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "kalle@lexicon.se",
                Email = "kalle@lexicon.se",
                Name = "Kalle Andersson",
                PhoneNumber = "073-0014523",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Brice.Lambson@lexicon.se",
                Email = "brice.lambson@lexicon.se",
                Name = "Brice Lambson",
                PhoneNumber = "",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "RowanMiller@lexicon.se",
                Email = "rowan.miller@lexicon.se",
                Name = "Rowan Miller",
                PhoneNumber = "",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "Annika.Pettersson@lexicon.se",
                Email = "annika.pettersson@lexicon.se",
                Name = "Annika Pettersson",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Karl.Gunnarsson@lexicon.se",
                Email = "karl.gunnarsson@lexicon.se",
                Name = "Karl Gunnarsson",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Henrik.Henriksson@lexicon.se",
                Email = "henrik.henriksson@lexicon.se",
                Name = "Henrik Henriksson",
                PhoneNumber = "",
                CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "paul.smith@lexicon.se",
                Email = "paul.smith@lexicon.se",
                Name = "Paul Smith",
                PhoneNumber = "",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "Karin.Svedin@lexicon.se",
                Email = "karin.svedin@lexicon.se",
                Name = "Karin Svedin",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Nicklas.Malm@lexicon.se",
                Email = "nicklas.malm@lexicon.se",
                Name = "Nicklas Malm",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Linda.Thatcher@lexicon.se",
                Email = "linda.thatcher@lexicon.se",
                Name = "Linda Thatcher",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Charles.deGaulle@lexicon.se",
                Email = "charles.degaulle@lexicon.se",
                Name = "Charles de Gaulle",
                PhoneNumber = "",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "Margret.Thatcher@lexicon.se",
                Email = "margret.thatcher@lexicon.se",
                Name = "Margret Thatcher",
                PhoneNumber = "",
                CourseId = 4
            },
            new ApplicationUser
            {
                UserName = "Winston.Churchill@lexicon.se",
                Email = "winston.churchill@lexicon.se",
                Name = "Winston Churchill",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Mahatma.Gandhi@lexicon.se",
                Email = "mahatma.gandhi@lexicon.se",
                Name = "Mahatma Gandhi",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Bill.Gates@lexicon.se",
                Email = "bill.gates@lexicon.se",
                Name = "Bill Gates",
                PhoneNumber = "",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "John.MKeynes@lexicon.se",
                Email = "john.mkeynes@lexicon.se",
                Name = "John M Keynes",
                PhoneNumber = "",
                CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "Mikhail.Gorbachev@lexicon.se",
                Email = "mikhail.gorbachev@lexicon.se",
                Name = "Mikhail Gorbachev",
                PhoneNumber = "",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "George.Orwell@lexicon.se",
                Email = "george.orwell@lexicon.se",
                Name = "George Orwell",
                PhoneNumber = "",
                CourseId = 4
            },
            new ApplicationUser
            {
                UserName = "Pablo.Picasso@lexicon.se",
                Email = "pablo.picasso@lexicon.se",
                Name = "Pablo Picasso",
                PhoneNumber = "",
                CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "Muhammad.Ali@lexicon.se",
                Email = "muhammad.ali@lexicon.se",
                Name = "Muhammad Ali",
                PhoneNumber = "",
                CourseId = 2
            },
           new ApplicationUser
            {
                UserName = "Louis.Pasteur@lexicon.se",
                Email = "louis.pasteur@lexicon.se",
                Name = "Louis Pasteur",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Lena.Karlsson@lexicon.se",
                Email = "lena.karlsson@lexicon.se",
                Name = "Lena Karlsson",
                PhoneNumber = "",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "Lena.Karlsson2@lexicon.se",
                Email = "lena.karlsson2@lexicon.se",
                Name = "Lena Karlsson",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Charles.Darwin@lexicon.se",
                Email = "charles.darwin@lexicon.se",
                Name = "Charles Darwin",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Per.Karlsson@lexicon.se",
                Email = "per.karlsson@lexicon.se",
                Name = "Per Karlsson",
                PhoneNumber = "",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "Svante.Johnsson@lexicon.se",
                Email = "svante.johnsson@lexicon.se",
                Name = "Svante Johnsson",
                PhoneNumber= "",
                CourseId = 4
            },
            new ApplicationUser
            {
                UserName = "Leo.Tolstoy@lexicon.se",
                Email = "leo.tolstoy@lexicon.se",
                Name = "Leo Tolstoy",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Peter.Taylor@lexicon.se",
                Email = "peter.taylor@lexicon.se",
                Name = "Peter Taylor",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Chris.Harrison@lexicon.se",
                Email = "chris.harrison@lexicon.se",
                Name = "Chris Harrison",
                PhoneNumber = "",
                CourseId = 1
            },
           new ApplicationUser
            {
                UserName = "Elvis.Presley@lexicon.se",
                Email = "elvis.presley@lexicon.se",
                Name = "Elvis Presley",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Christopher.Columbus@lexicon.se",
                Email = "christopher.columbus@lexicon.se",
                Name = "Christopher Columbus",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Roger.Karlsson@lexicon.se",
                Email = "roger.karlsson@lexicon.se",
                Name = "Roger Karlsson",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Gunilla.Berg@lexicon.se",
                Email = "gunilla.ber@lexicon.se",
                Name = "Gunilla Berg",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Paul.McCartney@lexicon.se",
                Email = "paul.mccartney@lexicon.se",
                Name = "Paul McCartney",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Thomas.Edisson@lexicon.se",
                Email = "thomas.edisson@lexicon.se",
                Name = "Thomas Edisson",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Aung.SanSuuKyi@lexicon.se",
                Email = "aung.sansuukyi@lexicon.se",
                Name = "Aung San Suu Kyi",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Albert.Einstein@lexicon.se",
                Email = "albert.einstein@lexicon.se",
                Name = "Albert Einstein",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Nils.Nilsson@lexicon.se",
                Email = "nils.nilsson@lexicon.se",
                Name = "Nils Nilsson",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Jonas.Helmersson@lexicon.se",
                Email = "jonas.helmersson@lexicon.se",
                Name = "Jonas Helmersson",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "JK.Rowling@lexicon.se",
                Email = "jk.rowling@lexicon.se",
                Name = "J.K. Rowling",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Lisa.Svensson@lexicon.se",
                Email = "lisa.svensson@lexicon.se",
                Name = "Lisa Svensson",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Pernilla.Lind@lexicon.se",
                Email = "pernilla.lind@lexicon.se",
                Name = "Pernilla Lind",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Barack.Obama@lexicon.se",
                Email = "barack.obama@lexicon.se",
                Name = "Barack Obama",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Richard.Branson@lexicon.se",
                Email = "richard.branson@lexicon.se",
                Name = "Richard Branson",
                PhoneNumber = "",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Holger.Pettersson@lexicon.se",
                Email = "holger.pettersson@lexicon.se",
                Name = "Holger Pettersson",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Fia.Nilsson@lexicon.se",
                Email = "fia.nilsson@lexicon.se",
                Name = "Fia Nilsson",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Thomas.Edison@lexicon.se",
                Email = "thomas.edison@lexicon.se",
                Name = "Thomas Edison",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Ulla.Ljung@lexicon.se",
                Email = "ulla.ljung@lexicon.se",
                Name = "Ulla Ljung",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Patsy.McBrian@lexicon.se",
                Email = "patsy.mcbrian@lexicon.se",
                Name = "Patsy McBrian",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Jens.Eliasson@lexicon.se",
                Email = "jens.eliasson@lexicon.se",
                Name = "Jens Eliasson",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Kalle.Svensson@lexicon.se",
                Email = "kalle.svensson@lexicon.se",
                Name = "Kalle Svensson",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Anne.Reasoner@lexicon.se",
                Email = "anne.reasoner@lexicon.se",
                Name = "Anne Reasoner",
                PhoneNumber = "",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Henry.Ford@lexicon.se",
                Email = "henry.ford@lexicon.se",
                Name = "Henry Ford",
                PhoneNumber = "",
                CourseId = 1
            }
        };


            return users;
        }

        private static void SeedDocuments(ApplicationDbContext context, ApplicationUser user)
        {
            //var document = new Document()
            //{
            //    DocumentId = 1,
            //    CourseId = 1,
            //    Description = "Schedule",
            //    Name = "Schedule",
            //    TimeStamp = DateTime.Now,
            //    Type = ".pdf",
            //    UserId = user.Id,
            //    DocumentType = DocumentType.Scheman
            //};
            var document = new Document()
            {
                DocumentId = 1,
                CourseId = 1,
                Description = "Föreläsning C#",
                Name = "Föreläsning C#.pptx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pptx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "fredrik@lexicon.se").Id,
                DocumentType = DocumentType.HandledningAktivitet
            };

            var document2 = new Document()
            {
                DocumentId = 2,
                CourseId = 1,
                Description = "Föreläsning Bootstrap",
                Name = "Föreläsning Bootstrap.pptx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pptx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "fredrik@lexicon.se").Id,
                DocumentType = DocumentType.HandledningAktivitet
            };

            var document3 = new Document()
            {
                DocumentId = 3,
                CourseId = 1,
                ModuleId = 2,
                Description = "Föreläsning HTML",
                Name = "Föreläsning HTML.pptx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pptx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.HandledningAktivitet
            };


            var document4 = new Document()
            {
                DocumentId = 4,
                CourseId = 1,
                ModuleId = 2,
                Description = "Föreläsning CSS",
                Name = "Föreläsning CSS.pptx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pptx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.HandledningAktivitet
            };

            var document5 = new Document()
            {
                DocumentId = 5,
                ModuleId = 2,
                Description = "Föreläsning AngularJS",
                Name = "Föreläsning AngularJS.pptx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pptx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "fredrik@lexicon.se").Id,
                DocumentType = DocumentType.HandledningAktivitet

            };

            var document6 = new Document()
            {
                DocumentId = 6,
                ModuleId = 2,
                Description = "Föreläsning JavaScript",
                Name = "Föreläsning JavaScript.pptx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pptx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.HandledningAktivitet
            };

            var document7 = new Document()
            {
                DocumentId = 7,
                ModuleId = 1,
                ActivityId =4,
                Description = "Övningsuppgift 1 - C#.",
                Name = "Övningsuppgift 1 - C#.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document8 = new Document()
            {
                DocumentId = 8,
                ModuleId = 1,
                ActivityId = 4,
                Description = "Övningsuppgift 2 - C#.",
                Name = "Övningsuppgift 2 - C#.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document9 = new Document()
            {
                DocumentId = 9,
                ModuleId = 1,
                ActivityId = 4,
                Description = "Övningsuppgift 3 - C#.",
                Name = "Övningsuppgift 3 - C#.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document10 = new Document()
            {
                DocumentId = 10,
                ModuleId = 1,
                ActivityId = 4,
                Description = "Övningsuppgift 4 - C#.",
                Name = "Övningsuppgift 4 - C#.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document11 = new Document()
            {
                DocumentId = 11,
                ModuleId = 2,
                //ActivityId = 4,
                Description = "Övningsuppgift 5 - HTML",
                Name = "Övningsuppgift 5 - HTML.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document12 = new Document()
            {
                DocumentId = 12,
                ModuleId = 2,
                //ActivityId = 4,
                Description = "Övningsuppgift 6 - AngularJS",
                Name = "Övningsuppgift 6 - AngularJS.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document13 = new Document()
            {
                DocumentId = 13,
                ModuleId = 2,
                //ActivityId = 4,
                Description = "Övningsuppgift 7 - JavaScript",
                Name = "Övningsuppgift 7 - JavaScript.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "fredrik@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document14 = new Document()
            {
                DocumentId = 14,
                CourseId = 1,
                Description = "Kursschema DotNET-Vår2016",
                Name = "Kursschema DotNET-Vår2016.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "anette@lexicon.se").Id,
                DocumentType = DocumentType.Scheman
            };

            var document15 = new Document()
            {
                DocumentId = 15,
                CourseId = 1,
                Description = "Klasslista DotNET-Vår2016 epost.pdf",
                Name = "Klasslista DotNET-Vår2016 epost.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "anette@lexicon.se").Id,
                DocumentType = DocumentType.Övrigt
            };

            var document16 = new Document()
            {
                DocumentId = 16,
                CourseId = 1,
                Description = "TorgInfo 2016-07-08",
                Name = "TorgInfo 2016-07-08.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "yaser@lexicon.se").Id,
                DocumentType = DocumentType.TorgInfo
            };

            var document17 = new Document()
            {
                DocumentId = 17,
                CourseId = 1,
                Description = "TorgInfo 2016-07-11",
                Name = "TorgInfo 2016-07-11.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "yaser@lexicon.se").Id,
                DocumentType = DocumentType.TorgInfo
            };

            var document18 = new Document()
            {
                DocumentId = 18,
                ModuleId = 1,
                ActivityId = 4,
                Description = "KalleAndersson_Inlämning_övning1",
                Name = "KalleAndersson_Inlämning_övning1.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document19 = new Document()
            {
                DocumentId = 19,
                ModuleId = 1,
                ActivityId = 4,
                Description = "KalleAndersson_Inlämning_övning2",
                Name = "KalleAndersson_Inlämning_övning2.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document20 = new Document()
            {
                DocumentId = 20,
                ModuleId = 1,
                ActivityId = 4,
                Description = "KalleAndersson_Inlämning_övning3",
                Name = "KalleAndersson_Inlämning_övning3.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document21 = new Document()
            {
                DocumentId = 21,
                ModuleId = 1,
                ActivityId = 4,
                Description = "KalleAndersson_Inlämning_övning4",
                Name = "KalleAndersson_Inlämning_övning4.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };


            var document22 = new Document()
            {
                DocumentId = 22,
                ModuleId = 2,
                //ActivityId = 4,
                Description = "KalleAndersson_Inlämning_övning5",
                Name = "KalleAndersson_Inlämning_övning5.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document23 = new Document()
            {
                DocumentId = 23,
                ModuleId = 2,
                Description = "KalleAndersson_Inlämning_övning6",
                Name = "KalleAndersson_Inlämning_övning6.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document24 = new Document()
            {
                DocumentId = 24,
                ModuleId = 2,
                Description = "KalleAndersson_Inlämning_övning7",
                Name = "KalleAndersson_Inlämning_övning7.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document25 = new Document()
            {
                DocumentId = 25,
                ModuleId = 2,
                Description = "KalleAndersson_Inlämning_övning8",
                Name = "KalleAndersson_Inlämning_övning8.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document26 = new Document()
            {
                DocumentId = 26,
                ModuleId = 2,
                //ActivityId = 4,
                Description = "Övningsuppgift 8 - BootStrap",
                Name = "Övningsuppgift 8 - BootStrap.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "fredrik@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };


            context.Documents.AddOrUpdate(d => d.DocumentId, document);
            context.Documents.AddOrUpdate(d => d.DocumentId, document2);
            context.Documents.AddOrUpdate(d => d.DocumentId, document3);
            context.Documents.AddOrUpdate(d => d.DocumentId, document4);
            context.Documents.AddOrUpdate(d => d.DocumentId, document5);
            context.Documents.AddOrUpdate(d => d.DocumentId, document6);
            context.Documents.AddOrUpdate(d => d.DocumentId, document7);
            context.Documents.AddOrUpdate(d => d.DocumentId, document8);
            context.Documents.AddOrUpdate(d => d.DocumentId, document9);
            context.Documents.AddOrUpdate(d => d.DocumentId, document10);
            context.Documents.AddOrUpdate(d => d.DocumentId, document11);
            context.Documents.AddOrUpdate(d => d.DocumentId, document12);
            context.Documents.AddOrUpdate(d => d.DocumentId, document13);
            context.Documents.AddOrUpdate(d => d.DocumentId, document14);
            context.Documents.AddOrUpdate(d => d.DocumentId, document15);
            context.Documents.AddOrUpdate(d => d.DocumentId, document16);
            context.Documents.AddOrUpdate(d => d.DocumentId, document17);
            context.Documents.AddOrUpdate(d => d.DocumentId, document18);
            context.Documents.AddOrUpdate(d => d.DocumentId, document19);
            context.Documents.AddOrUpdate(d => d.DocumentId, document20);
            context.Documents.AddOrUpdate(d => d.DocumentId, document21);
            context.Documents.AddOrUpdate(d => d.DocumentId, document22);
            context.Documents.AddOrUpdate(d => d.DocumentId, document23);
            context.Documents.AddOrUpdate(d => d.DocumentId, document24);
            context.Documents.AddOrUpdate(d => d.DocumentId, document25);
            context.Documents.AddOrUpdate(d => d.DocumentId, document26);
        }
    }
}

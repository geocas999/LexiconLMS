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

                if (user.UserName == "teacher@lexicon.se" || user.UserName == "george.caspersson@lexicon.se" || user.UserName == "anette@lexicon.se" || user.UserName == "yaser@lexicon.se" || user.UserName == "fredrik@lexicon.se")
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
                StartDate = DateTime.Now.AddDays(150)
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
                StartDate = DateTime.Now.AddDays(150)
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
                StartDate = DateTime.Now.AddDays(150)
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
                StartTime = DateTime.Now.AddDays(1),
                EndTime = DateTime.Now.AddDays(1),
                ModuleId = 1
            };
            var activity2 = new Activity()
            {
                ActivityId = 2,
                Description = "Pluralsight C# Fundamentals, del 2",
                Type = ActivityType.Elearning,
                StartTime = DateTime.Now.AddDays(2),
                EndTime = DateTime.Now.AddDays(2),
                ModuleId = 1
            };
            var activity3 = new Activity()
            {
                ActivityId = 3,
                Description = "C# lecture with Adrian",
                Type = ActivityType.Lecture,
                StartTime = DateTime.Now.AddDays(3),
                EndTime = DateTime.Now.AddDays(3),
                ModuleId = 1
            };
            var activity4 = new Activity()
            {
                ActivityId = 4,
                Description = "Pluralsight C# Fundamentals, del 3",
                Type = ActivityType.Elearning,
                StartTime = DateTime.Now.AddDays(4),
                EndTime = DateTime.Now.AddDays(4),
                ModuleId = 1
            };
            var activity5 = new Activity()
            {
                ActivityId = 5,
                Description = "Pluralsight C# Fundamentals, del 4",
                Type = ActivityType.Elearning,
                StartTime = DateTime.Now.AddDays(5),
                EndTime = DateTime.Now.AddDays(5),
                ModuleId = 1
            };
            var activity6 = new Activity()
            {
                ActivityId = 6,
                Description = "C# lecture with Adrian",
                Type = ActivityType.Lecture,
                StartTime = DateTime.Now.AddDays(6),
                EndTime = DateTime.Now.AddDays(6),
                ModuleId = 1
            };
       
            var activity7 = new Activity()
            {
                ActivityId = 6,
                Description = "Pluralsight HTML5 & CSS",
                Type = ActivityType.Elearning,
                StartTime = DateTime.Now.AddDays(15),
                EndTime = DateTime.Now.AddDays(15),
                ModuleId = 2
            };

            context.Activities.AddOrUpdate(activity);
            context.Activities.AddOrUpdate(activity2);
            context.Activities.AddOrUpdate(activity3);
            context.Activities.AddOrUpdate(activity4);
            context.Activities.AddOrUpdate(activity5);
            context.Activities.AddOrUpdate(activity6);
            context.Activities.AddOrUpdate(activity7);
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
                PhoneNumber = "070-50011223",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "teacher@lexicon.se",
                Email = "teacher@lexicon.se",
                Name = "Teacher Teachersson",
                PhoneNumber = "073-5513832",
                //CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "fredrik@lexicon.se",
                Email = "fredrik@lexicon.se",
                Name = "Fredrik Lindroth",
                PhoneNumber = "070-2551823",
                //CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "anette@lexicon.se",
                Email = "anette@lexicon.se",
                Name = "Anette Tillbom",
                PhoneNumber = "076-7788931",
                //CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "yaser@lexicon.se",
                Email = "yaser@lexicon.se",
                Name = "Yaser Mosavi",
                PhoneNumber = "070-6021523",
                //CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "george.caspersson@lexicon.se",
                Email = "george.caspersson@lexicon.se",
                Name = "George Caspersson",
                PhoneNumber = "076-2053189",
                //CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "kalle.andersson@lexicon.se",
                Email = "kalle.andersson@lexicon.se",
                Name = "Kalle Andersson",
                PhoneNumber = "073-6014520",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "sven.gustafsson@lexicon.se",
                Email = "sven.gustafsson@lexicon.se",
                Name = "Sven Gustafsson",
                PhoneNumber = "073-6614525",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "nils.eriksson@lexicon.se",
                Email = "nils.eriksson@lexicon.se",
                Name = "Nils Eriksson",
                PhoneNumber = "073-4414527",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "lisa.andersson@lexicon.se",
                Email = "lisa.andersson@lexicon.se",
                Name = "Lisa Andersson",
                PhoneNumber = "073-02214529",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "Annika.Pettersson@lexicon.se",
                Email = "annika.pettersson@lexicon.se",
                Name = "Annika Pettersson",
                PhoneNumber = "070-89213213",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Karl.Gunnarsson@live.com",
                Email = "karl.gunnarsson@live.com",
                Name = "Karl Gunnarsson",
                PhoneNumber = "071-8903322",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Henrik.Henriksson@telia.se",
                Email = "henrik.henriksson@telia.se",
                Name = "Henrik Henriksson",
                PhoneNumber = "073-4548887",
                CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "paul.smith@yahoo.com",
                Email = "paul.smith@yahoo.com",
                Name = "Paul Smith",
                PhoneNumber = "08-3654987",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "Karin.Svedin@bredband.net",
                Email = "karin.svedin@bredband.net",
                Name = "Karin Svedin",
                PhoneNumber = "08-45668522",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Nicklas.Malm@bredband.net",
                Email = "nicklas.malm@bredband.net",
                Name = "Nicklas Malm",
                PhoneNumber = "05-7864328",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "Linda.Thatcher@bredband.net",
                Email = "linda.thatcher@bredband.net",
                Name = "Linda Thatcher",
                PhoneNumber = "08-4680082",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Charles.deGaulle@hotmail.com",
                Email = "charles.degaulle@hotmail.com",
                Name = "Charles de Gaulle",
                PhoneNumber = "070-9761674",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "Margret.Thatcher@bredband.net",
                Email = "margret.thatcher@bredband.nete",
                Name = "Margret Thatcher",
                PhoneNumber = "072-6791254",
                CourseId = 4
            },
            new ApplicationUser
            {
                UserName = "Winston.Churchill@bahnhof.se",
                Email = "winston.churchill@bahnhof.se",
                Name = "Winston Churchill",
                PhoneNumber = "075-6314994",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Mahatma.Gandhi@hotmail.com",
                Email = "mahatma.gandhi@hotmail.com",
                Name = "Mahatma Gandhi",
                PhoneNumber = "073-6408933",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "Bill.Gates@bahnhof.se",
                Email = "bill.gates@bahnhof.se",
                Name = "Bill Gates",
                PhoneNumber = "070-6302045",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "John.MKeynes@comhem.se",
                Email = "john.mkeynes@comhem.se",
                Name = "John M Keynes",
                PhoneNumber = "072-6895573",
                CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "Mikhail.Gorbachev@tele2.se",
                Email = "mikhail.gorbachev@tele2.se",
                Name = "Mikhail Gorbachev",
                PhoneNumber = "08-5804678",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "George.Orwell@comhem.se",
                Email = "george.orwell@comhem.se",
                Name = "George Orwell",
                PhoneNumber = "08-5302211",
                CourseId = 4
            },
            new ApplicationUser
            {
                UserName = "Pablo.Picasso@glocalnet.net",
                Email = "pablo.picasso@glocalnet.net",
                Name = "Pablo Picasso",
                PhoneNumber = "076-7235590",
                CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "Muhammad.Ali@tele2.se",
                Email = "muhammad.ali@tele2.se",
                Name = "Muhammad Ali",
                PhoneNumber = "08-2548937",
                CourseId = 2
            },
           new ApplicationUser
            {
                UserName = "Louis.Pasteur@comhem.se",
                Email = "louis.pasteur@comhem.se",
                Name = "Louis Pasteur",
                PhoneNumber = "08-7025490",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Lena.Karlsson@glocalnet.net",
                Email = "lena.karlsson@glocalnet.net",
                Name = "Lena Karlsson",
                PhoneNumber = "08-7608914",
                CourseId = 5
            },
            new ApplicationUser
            {
                UserName = "Lena.Karlsson2@spray.se",
                Email = "lena.karlsson2@spray.se",
                Name = "Lena Karlsson",
                PhoneNumber = "08-7206230",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Charles.Darwin@spray.se",
                Email = "charles.darwin@spray.se",
                Name = "Charles Darwin",
                PhoneNumber = "071-69812457",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Per.Karlsson@microsoft.se",
                Email = "per.karlsson@microsoft.se",
                Name = "Per Karlsson",
                PhoneNumber = "076-9380506",
                CourseId = 2
            },
            new ApplicationUser
            {
                UserName = "Svante.Johnsson@glocalnet.net",
                Email = "svante.johnsson@glocalnet.net",
                Name = "Svante Johnsson",
                PhoneNumber= "070-6408277",
                CourseId = 4
            },
            new ApplicationUser
            {
                UserName = "Leo.Tolstoy@microsoft.se",
                Email = "leo.tolstoy@microsoft.se",
                Name = "Leo Tolstoy",
                PhoneNumber = "08-3502225",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Peter.Taylor@mail2web.com",
                Email = "peter.taylor@mail2web.com",
                Name = "Peter Taylor",
                PhoneNumber = "08-5604512",
                CourseId = 4
            },
            new ApplicationUser
            {
                UserName = "Chris.Harrison@bredbandsbolaget.se",
                Email = "chris.harrison@bredbandsbolaget.se",
                Name = "Chris Harrison",
                PhoneNumber = "08-7206643",
                CourseId = 6
            },
           new ApplicationUser
            {
                UserName = "Elvis.Presley@mail2web.com",
                Email = "elvis.presley@mail2web.com",
                Name = "Elvis Presley",
                PhoneNumber = "070-5506677",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Christopher.Columbus@lexicon.se",
                Email = "christopher.columbus@lexicon.se",
                Name = "Christopher Columbus",
                PhoneNumber = "070-6305574",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Roger.Karlsson@mail2web.com",
                Email = "roger.karlsson@mail2web.com",
                Name = "Roger Karlsson",
                PhoneNumber = "08-6503387",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Gunilla.Berg@bredbandsbolaget.se",
                Email = "gunilla.ber@bredbandsbolaget.se",
                Name = "Gunilla Berg",
                PhoneNumber = "08-6302271",
                CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "Paul.McCartney@comhem.se",
                Email = "paul.mccartney@comhem.se",
                Name = "Paul McCartney",
                PhoneNumber = "08-9305681",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Thomas.Edisson@yahoo.com",
                Email = "thomas.edisson@yahoo.com",
                Name = "Thomas Edisson",
                PhoneNumber = "070-86071982",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Aung.SanSuuKyi@comhem.se",
                Email = "aung.sansuukyi@comhem.se",
                Name = "Aung San Suu Kyi",
                PhoneNumber = "08-6873342",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Albert.Einstein@bredbandsbolaget.se",
                Email = "albert.einstein@bredbandsbolaget.se",
                Name = "Albert Einstein",
                PhoneNumber = "071-7496681",
                CourseId = 1
            },
            new ApplicationUser
            {
                UserName = "Nils.Nilsson@yahoo.se",
                Email = "nils.nilsson@yahoo.se",
                Name = "Nils Nilsson",
                PhoneNumber = "08-6389917",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Jonas.Helmersson@comhem.se",
                Email = "jonas.helmersson@comhem.se",
                Name = "Jonas Helmersson",
                PhoneNumber = "08-48902211",
                CourseId = 4
            },
            new ApplicationUser
            {
                UserName = "JK.Rowling@live.com",
                Email = "jk.rowling@live.com",
                Name = "J.K. Rowling",
                PhoneNumber = "08-79821113",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Lisa.Svensson@hotmail.se",
                Email = "lisa.svensson@hotmail.se",
                Name = "Lisa Svensson",
                PhoneNumber = "076-2595578",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Pernilla.Lind@live.com",
                Email = "pernilla.lind@live.com",
                Name = "Pernilla Lind",
                PhoneNumber = "071-4508811",
                CourseId = 3
            },
            new ApplicationUser
            {
                UserName = "Barack.Obama@comhem.se",
                Email = "barack.obama@comhem.se",
                Name = "Barack Obama",
                PhoneNumber = "070-6487899",
                CourseId = 6
            },
            new ApplicationUser
            {
                UserName = "Richard.Branson@live.come",
                Email = "richard.branson@live.com",
                Name = "Richard Branson",
                PhoneNumber = "070-6847724",
                CourseId = 2
            },

            // Commented out CourseId = 1 Users
            //new ApplicationUser
            //{
            //    UserName = "Holger.Pettersson@lexicon.se",
            //    Email = "holger.pettersson@lexicon.se",
            //    Name = "Holger Pettersson",
            //    PhoneNumber = "",
            //    CourseId = 1
            //},
            //new ApplicationUser
            //{
            //    UserName = "Fia.Nilsson@lexicon.se",
            //    Email = "fia.nilsson@lexicon.se",
            //    Name = "Fia Nilsson",
            //    PhoneNumber = "",
            //    CourseId = 1
            //},
            //new ApplicationUser
            //{
            //    UserName = "Thomas.Edison@lexicon.se",
            //    Email = "thomas.edison@lexicon.se",
            //    Name = "Thomas Edison",
            //    PhoneNumber = "",
            //    CourseId = 1
            //},
            //new ApplicationUser
            //{
            //    UserName = "Ulla.Ljung@lexicon.se",
            //    Email = "ulla.ljung@lexicon.se",
            //    Name = "Ulla Ljung",
            //    PhoneNumber = "",
            //    CourseId = 1
            //},
            //new ApplicationUser
            //{
            //    UserName = "Patsy.McBrian@lexicon.se",
            //    Email = "patsy.mcbrian@lexicon.se",
            //    Name = "Patsy McBrian",
            //    PhoneNumber = "",
            //    CourseId = 1
            //},
            //new ApplicationUser
            //{
            //    UserName = "Jens.Eliasson@lexicon.se",
            //    Email = "jens.eliasson@lexicon.se",
            //    Name = "Jens Eliasson",
            //    PhoneNumber = "",
            //    CourseId = 1
            //},
            //new ApplicationUser
            //{
            //    UserName = "Kalle.Svensson@lexicon.se",
            //    Email = "kalle.svensson@lexicon.se",
            //    Name = "Kalle Svensson",
            //    PhoneNumber = "",
            //    CourseId = 1
            //},
            //new ApplicationUser
            //{
            //    UserName = "Anne.Reasoner@lexicon.se",
            //    Email = "anne.reasoner@lexicon.se",
            //    Name = "Anne Reasoner",
            //    PhoneNumber = "",
            //    CourseId = 1
            //},
            //new ApplicationUser
            //{
            //    UserName = "Henry.Ford@lexicon.se",
            //    Email = "henry.ford@lexicon.se",
            //    Name = "Henry Ford",
            //    PhoneNumber = "",
            //    CourseId = 1
            //}
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
                ModuleId = 1,
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
                ModuleId = 2,
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
                ModuleId = 2,
                Description = "Föreläsning HTML",
                Name = "Föreläsning HTML.pptx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pptx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
                DocumentType = DocumentType.HandledningAktivitet
            };


            var document4 = new Document()
            {
                DocumentId = 4,
                ModuleId = 2,
                Description = "Föreläsning CSS",
                Name = "Föreläsning CSS.pptx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pptx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
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
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
                DocumentType = DocumentType.HandledningAktivitet
            };

            var document7 = new Document()
            {
                DocumentId = 7,
                ActivityId = 6,
                Description = "C# - Exercise 1",
                Name = "C#-exercise1.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                Deadline = DateTime.Now.AddDays(1),
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document8 = new Document()
            {
                DocumentId = 8,
                ActivityId = 6,
                Description = "C# - Exercise 1",
                Name = "C#-exercise2.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                Deadline = DateTime.Now.AddDays(-2),
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document9 = new Document()
            {
                DocumentId = 9,
                ActivityId = 4,
                Description = "Övningsuppgift 3 - C#.",
                Name = "Övningsuppgift 3 - C#.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                Deadline = DateTime.Now.AddDays(1),
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document10 = new Document()
            {
                DocumentId = 10,
                ActivityId = 4,
                Description = "Övningsuppgift 4 - C#.",
                Name = "Övningsuppgift 4 - C#.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                Deadline = DateTime.Now.AddDays(1),
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document11 = new Document()
            {
                DocumentId = 11,
                ActivityId = 5,
                Description = "Övningsuppgift 5",
                Name = "Övningsuppgift 5.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document12 = new Document()
            {
                DocumentId = 12,
                ActivityId = 5,
                Description = "Övningsuppgift 6",
                Name = "Övningsuppgift 6.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "george.caspersson@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document13 = new Document()
            {
                DocumentId = 13,
                ActivityId = 5,
                Description = "Övningsuppgift 7",
                Name = "Övningsuppgift 7.docx",
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
                ActivityId = 6,
                Description = "KalleAndersson_Inlämning_övning1",
                Name = "KalleAndersson_Inlämning_övning1.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document19 = new Document()
            {
                DocumentId = 19,
                ActivityId = 6,
                Description = "KalleAndersson_Inlämning_övning2",
                Name = "KalleAndersson_Inlämning_övning2.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document20 = new Document()
            {
                DocumentId = 20,
                ActivityId = 4,
                Description = "KalleAndersson_Inlämning_övning3",
                Name = "KalleAndersson_Inlämning_övning3.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document21 = new Document()
            {
                DocumentId = 21,
                ActivityId = 4,
                Description = "KalleAndersson_Inlämning_övning4",
                Name = "KalleAndersson_Inlämning_övning4.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };


            var document22 = new Document()
            {
                DocumentId = 22,
                ActivityId = 5,
                Description = "KalleAndersson_Inlämning_övning5",
                Name = "KalleAndersson_Inlämning_övning5.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document23 = new Document()
            {
                DocumentId = 23,
                ActivityId = 5,
                Description = "KalleAndersson_Inlämning_övning6",
                Name = "KalleAndersson_Inlämning_övning6.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document24 = new Document()
            {
                DocumentId = 24,
                ActivityId = 5,
                Description = "KalleAndersson_Inlämning_övning7",
                Name = "KalleAndersson_Inlämning_övning7.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document25 = new Document()
            {
                DocumentId = 25,
                ActivityId = 5,
                Description = "KalleAndersson_Inlämning_övning8",
                Name = "KalleAndersson_Inlämning_övning8.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "kalle.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document26 = new Document()
            {
                DocumentId = 26,
                ActivityId = 5,
                Description = "Övningsuppgift 8",
                Name = "Övningsuppgift 8.docx",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "fredrik@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document27 = new Document()
            {
                DocumentId = 27,
                ActivityId = 1,
                Description = "Pluralsight C# Fundamentals, del 1",
                Name = "Pluralsight C# Fundamentals, del 1.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "fredrik@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            var document28 = new Document()
            {
                DocumentId = 28,
                ActivityId = 2,
                Description = "Pluralsight C# Fundamentals, del 2",
                Name = "Pluralsight C# Fundamentals, del 2.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".docx",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "fredrik@lexicon.se").Id,
                DocumentType = DocumentType.Övningsuppgift
            };

            // Lisa Andersson
            var document29 = new Document()
            {
                DocumentId = 29,
                ActivityId = 6,
                Description = "LisaAndersson_Inlämning_övning1",
                Name = "LisaAndersson_Inlämning_övning1.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "lisa.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document30 = new Document()
            {
                DocumentId = 30,
                ActivityId = 4,
                Description = "LisaAndersson_Inlämning_övning2",
                Name = "LisaAndersson_Inlämning_övning2.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "lisa.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document31 = new Document()
            {
                DocumentId = 31,
                ActivityId = 4,
                Description = "LisaAndersson_Inlämning_övning3",
                Name = "LisaAndersson_Inlämning_övning3.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "lisa.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document32 = new Document()
            {
                DocumentId = 32,
                ActivityId = 4,
                Description = "LisaAndersson_Inlämning_övning4",
                Name = "LisaAndersson_Inlämning_övning4.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "lisa.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };


            var document33 = new Document()
            {
                DocumentId = 33,
                ActivityId = 5,
                Description = "LisaAndersson_Inlämning_övning5",
                Name = "LisaAndersson_Inlämning_övning5.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "lisa.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document34 = new Document()
            {
                DocumentId = 34,
                ActivityId = 5,
                Description = "LisaAndersson_Inlämning_övning6",
                Name = "LisaAndersson_Inlämning_övning6.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "lisa.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document35 = new Document()
            {
                DocumentId = 35,
                ActivityId = 5,
                Description = "LisaAndersson_Inlämning_övning7",
                Name = "LisaAndersson_Inlämning_övning7.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "lisa.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document36 = new Document()
            {
                DocumentId = 36,
                ActivityId = 5,
                Description = "LisaAndersson_Inlämning_övning8",
                Name = "LisaAndersson_Inlämning_övning8.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "lisa.andersson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            // End Lisa


            // Sven Gustafsson
            var document37 = new Document()
            {
                DocumentId = 37,
                ActivityId = 6,
                Description = "Sven Gustafsson_Inlämning_övning1",
                Name = "Sven Gustafsson_Inlämning_övning1.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "sven.gustafsson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document38 = new Document()
            {
                DocumentId = 38,
                ActivityId = 4,
                Description = "Sven Gustafsson_Inlämning_övning2",
                Name = "Sven Gustafsson_Inlämning_övning2.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "sven.gustafsson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document39 = new Document()
            {
                DocumentId = 39,
                ActivityId = 4,
                Description = "Sven Gustafsson_Inlämning_övning3",
                Name = "Sven Gustafsson_Inlämning_övning3.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "sven.gustafsson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document40 = new Document()
            {
                DocumentId = 40,
                ActivityId = 4,
                Description = "Sven Gustafsson_Inlämning_övning4",
                Name = "Sven Gustafsson_Inlämning_övning4.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "sven.gustafsson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };


            var document41 = new Document()
            {
                DocumentId = 41,
                ActivityId = 5,
                Description = "Sven Gustafsson_Inlämning_övning5",
                Name = "Sven Gustafsson_Inlämning_övning5.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "sven.gustafsson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document42 = new Document()
            {
                DocumentId = 42,
                ActivityId = 5,
                Description = "Sven Gustafsson_Inlämning_övning6",
                Name = "Sven Gustafsson_Inlämning_övning6.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "sven.gustafsson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document43 = new Document()
            {
                DocumentId = 43,
                ActivityId = 5,
                Description = "Sven Gustafsson_Inlämning_övning7",
                Name = "Sven Gustafsson_Inlämning_övning7.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "sven.gustafsson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document44 = new Document()
            {
                DocumentId = 44,
                ActivityId = 5,
                Description = "Sven Gustafsson_Inlämning_övning8",
                Name = "Sven Gustafsson_Inlämning_övning8.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "sven.gustafsson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            // End Sven


            // Nils Eriksson
            var document45 = new Document()
            {
                DocumentId = 45,
                ActivityId = 6,
                Description = "Nils Eriksson_Inlämning_övning1",
                Name = "Nils Eriksson_Inlämning_övning1.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "nils.eriksson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document46 = new Document()
            {
                DocumentId = 46,
                ActivityId = 4,
                Description = "Nils Eriksson_Inlämning_övning2",
                Name = "Nils Eriksson_Inlämning_övning2.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "nils.eriksson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document47 = new Document()
            {
                DocumentId = 47,
                ActivityId = 4,
                Description = "Nils Eriksson_Inlämning_övning3",
                Name = "Nils Eriksson_Inlämning_övning3.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "nils.eriksson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document48 = new Document()
            {
                DocumentId = 48,
                ActivityId = 4,
                Description = "Nils Eriksson_Inlämning_övning4",
                Name = "Nils Eriksson_Inlämning_övning4.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "nils.eriksson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };


            var document49 = new Document()
            {
                DocumentId = 49,
                ActivityId = 5,
                Description = "Nils Eriksson_Inlämning_övning5",
                Name = "Nils Eriksson_Inlämning_övning5.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "nils.eriksson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document50 = new Document()
            {
                DocumentId = 50,
                ActivityId = 5,
                Description = "Nils Eriksson_Inlämning_övning6",
                Name = "Nils Eriksson_Inlämning_övning6.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "nils.eriksson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document51 = new Document()
            {
                DocumentId = 51,
                ActivityId = 5,
                Description = "Nils Eriksson_Inlämning_övning7",
                Name = "Nils Eriksson_Inlämning_övning7.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "nils.eriksson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };

            var document52 = new Document()
            {
                DocumentId = 52,
                ActivityId = 5,
                Description = "Nils Eriksson_Inlämning_övning8",
                Name = "Nils Eriksson_Inlämning_övning8.pdf",
                FilePath = "~/LMSDocuments",
                TimeStamp = DateTime.Now,
                //Type = ".pdf",
                UserId = context.Users.FirstOrDefault(u => u.UserName == "nils.eriksson@lexicon.se").Id,
                DocumentType = DocumentType.Inlämningsuppgift
            };
            // End Nils




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
            context.Documents.AddOrUpdate(d => d.DocumentId, document27);
            context.Documents.AddOrUpdate(d => d.DocumentId, document28);
            context.Documents.AddOrUpdate(d => d.DocumentId, document29);
            context.Documents.AddOrUpdate(d => d.DocumentId, document30);
            context.Documents.AddOrUpdate(d => d.DocumentId, document31);
            context.Documents.AddOrUpdate(d => d.DocumentId, document32);
            context.Documents.AddOrUpdate(d => d.DocumentId, document33);
            context.Documents.AddOrUpdate(d => d.DocumentId, document34);
            context.Documents.AddOrUpdate(d => d.DocumentId, document35);
            context.Documents.AddOrUpdate(d => d.DocumentId, document36);
            context.Documents.AddOrUpdate(d => d.DocumentId, document37);
            context.Documents.AddOrUpdate(d => d.DocumentId, document38);
            context.Documents.AddOrUpdate(d => d.DocumentId, document39);
            context.Documents.AddOrUpdate(d => d.DocumentId, document40);
            context.Documents.AddOrUpdate(d => d.DocumentId, document41);
            context.Documents.AddOrUpdate(d => d.DocumentId, document42);
            context.Documents.AddOrUpdate(d => d.DocumentId, document43);
            context.Documents.AddOrUpdate(d => d.DocumentId, document44);
            context.Documents.AddOrUpdate(d => d.DocumentId, document45);
            context.Documents.AddOrUpdate(d => d.DocumentId, document46);
            context.Documents.AddOrUpdate(d => d.DocumentId, document47);
            context.Documents.AddOrUpdate(d => d.DocumentId, document48);
            context.Documents.AddOrUpdate(d => d.DocumentId, document49);
            context.Documents.AddOrUpdate(d => d.DocumentId, document50);
            context.Documents.AddOrUpdate(d => d.DocumentId, document51);
            context.Documents.AddOrUpdate(d => d.DocumentId, document52);
        }
    }
}

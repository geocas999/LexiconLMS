using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class TeacherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teacher
        public ActionResult TeacherOverview()
        {
            TeacherViewModels teacherViewModel = new TeacherViewModels();
            teacherViewModel.Courses = new List<Course>();
            teacherViewModel.Users = new List<ApplicationUser>();

            teacherViewModel.Courses = db.Courses.ToList();
            teacherViewModel.Users = db.Users.ToList();

            return View(teacherViewModel);
        }
        
        //2016-07-01 Anette - Link navbar Users
        //2016-07-05 Anette - Sort Order UserName and CourseName
        // GET: /Teacher/Users
        public ActionResult Users(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CourseSortParm = sortOrder == "Course.Name" ? "course_desc" : "Course.Name";
            var users = from s in db.Users
                           select s;
            switch (sortOrder)
            {
                case "name_desc":
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case "Course.Name":
                    users = users.OrderBy(s => s.Course.Name);
                    break;
                case "course_desc":
                    users = users.OrderByDescending(s => s.Course.Name);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }
            return View(users.ToList());

        }
    }
}
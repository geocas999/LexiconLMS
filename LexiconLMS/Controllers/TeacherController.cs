using LexiconLMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LexiconLMS.Controllers
{
    public class TeacherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Teacher
        public ActionResult Index()
        {
            TeacherViewModels teacherViewModel = new TeacherViewModels();
            teacherViewModel.Courses = new List<Course>();
            teacherViewModel.Users = new List<ApplicationUser>();

            teacherViewModel.Courses = db.Courses.ToList();
            teacherViewModel.Users = db.Users.ToList();

            return View(teacherViewModel);
        }

    }
}
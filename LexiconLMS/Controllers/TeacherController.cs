﻿using LexiconLMS.Models;
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
        //2016-07-04 Anette - Sort Order UserName and CourseName
        //2016-07-05 Anette - Search

        // GET: /Teacher/Users
        public ViewResult Users(string sortOrder, string searchString)
        {            
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CourseNameSortParm = sortOrder == "Course.Name" ? "course_desc" : "Course.Name";
            ViewBag.CourseIdSortParm = sortOrder == "Course.Id" ? "courseId_desc" : "Course.Id";
            var users = from s in db.Users
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.Contains(searchString)
                                        || s.Course.Name.Contains(searchString));
            }


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
                case "Course.Id":
                    users = users.OrderBy(s => s.Course.CourseId);
                    break;
                case "courseId_desc":
                    users = users.OrderByDescending(s => s.Course.CourseId);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }
            return View(users.ToList());
        }
        public ActionResult UserDetails (string id)
        {
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LexiconLMS.Models
{

    public class TeacherViewModels
    {
        [Display(Name = "Course name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; }

        public List<Course> Courses { get; set; }

        [Display(Name = "Name")]
        public string UserName { get; set; }
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }


}
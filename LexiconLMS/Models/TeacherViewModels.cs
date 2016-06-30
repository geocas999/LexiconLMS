using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    
    public class TeacherViewModels
	{
        //public int? CourseId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public List<Course> Courses { get; set; }

        //public string UserId { get; set; }
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public List<ApplicationUser> Users { get; set; }

    }

    
}
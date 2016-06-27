using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Course
    {
        public int CourseId{ get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate {get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [Display(Name="Course name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
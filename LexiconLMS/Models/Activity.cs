using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public ActivityType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }

        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }

    public enum ActivityType { Lecture, Exercise, Elearning, Presentation}
}
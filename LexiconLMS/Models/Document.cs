using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace LexiconLMS.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual Course Course { get; set; }
        public int? CourseId { get; set; }

        public virtual Module Module { get; set; }
        public int? ModuleId { get; set; }

        public virtual Activity Activity { get; set; }
        public int? ActivityId { get; set; }
    }
}
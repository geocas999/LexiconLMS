using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class AddDocumentToCourseModel
    {
        [Required]
        [Display(Name = "DocumentId")]
        public int DocumentId { get; set; }

        [Required]
        [DataType(DataType.Text )]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "TimeStamp")]
        public DateTime TimeStamp { get; set; }

        public int CourseId { get; set; }
        public string UserId { get; set; }

    }
}
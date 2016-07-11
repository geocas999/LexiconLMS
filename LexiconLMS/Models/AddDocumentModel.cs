using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class AddDocumentModel
    {
        [Required]
        [Display(Name = "DocumentType")]
        public string DocumentType { get; set; }

        public HttpPostedFileBase UploadedFile { get; set; }

        public string Description { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }


        //public string ModuleId { get; set; }
        //public string CourseId { get; set; }
        //public string ActivityId { get; set; }
        //public DateTime Timestamp { get; set; }

    }
}
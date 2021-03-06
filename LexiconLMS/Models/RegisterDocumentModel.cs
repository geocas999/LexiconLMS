﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace LexiconLMS.Models
{
    public enum DocumentType
    {
        HandledningAktivitet = 0,
        Inlämningsuppgift = 1,
        Övningsuppgift = 2,
        AnteckningarAktivitet = 3,
        TorgInfo = 4,
        Scheman = 5,
        Övrigt = 6
    }

    public class RegisterDocumentModel
    {

        public HttpPostedFileBase UploadedFile { get; set; }
       
        [Display(Name = "FilePath")]
        public string FilePath { get; set; }

        //[Required]
        [Display(Name = "DocumentType")]
        public DocumentType DocumentType { get; set; }

        //[Required]
        //[Display(Name = "DocumentId")]
        //public int DocumentId { get; set; }

        //[Required]
        [DataType(DataType.Text )]
        [Display(Name = "Name")]
        public string Name { get; set; }

        //[Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string Type { get; set; }

        //[Required]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //[Required]
        [DataType(DataType.Time)]
        [Display(Name = "TimeStamp")]
        public DateTime TimeStamp { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Deadline")]
        public DateTime? Deadline { get; set; }

        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }
        public string UserId { get; set; }
    }
}
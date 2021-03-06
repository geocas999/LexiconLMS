﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LexiconLMS.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }

        [Display(Name = "Activity name")]
        public string Name { get; set; }
        public ActivityType Type { get; set; }

        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [Display(Name = "End date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }

        public string Description { get; set; }

        public int ModuleId { get; set; }
        public virtual Module Module { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }

    public enum ActivityType
    {
        Lecture,
        Exercise,
        Elearning,
        Presentation
    }
}
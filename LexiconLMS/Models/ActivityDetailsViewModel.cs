using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LexiconLMS.Models
{
    public class ActivityDetailsViewModel
    {
        public Activity Acticity { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Document> StudentExercises { get; set; }
    }
}
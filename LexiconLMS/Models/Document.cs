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

        //2016-06-30, YM: Nedan kod hämtat från : http://stackoverflow.com/questions/21466258/mvc-5-code-first-userid-as-a-foreign-key
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        //2016-06-30, YM: ovan kod hämtat från : http://stackoverflow.com/questions/21466258/mvc-5-code-first-userid-as-a-foreign-key

        public int? CourseId { get; set; }
        public int? ModuleId { get; set; }
        public int? ActivityId { get; set; }

    }
}
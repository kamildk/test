using recenzent.Data.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace recenzent.Models
{
    public class PublicationPanelViewModel
    {

        public string Title { get; set; }
        public int PublicationId { get; set; }
        public ICollection<string> Tags { get; set; }
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> Sources { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public string NewCommentText { get; set; }
        public float Rating { get; set; }
        public int RateFromCurrUser { get; set; }
        public int ParentCommentId { get; set; }

        public PublicationPanelViewModel()
        {
            Tags = new List<string>();
            Sources = new List<string>();
            //Comments = new List<Comment>();
        }

    }
}

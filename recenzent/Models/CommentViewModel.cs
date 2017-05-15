using recenzent.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recenzent.Models
{
    public class CommentViewModel
    {
        public CommentViewModel() { }
        public CommentViewModel(Comment comment)
        {
            Comment = comment;
        }
        public Comment Comment { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<CommentViewModel> ChildReplies { get; set; }
        public string Body { get; set; }
        public int Id { get; set; }
        public int ParentReplyId { get; set; }
        public string UserName { get; set; }

    }
}
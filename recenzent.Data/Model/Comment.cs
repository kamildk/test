using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Comment {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Publication Publication { get; set; }

        [Required]
        public User User { get; set; }

        public Comment ParentComment { get; set; }
        public ICollection<Comment> ChildComments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Comment {
        public int CommentId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}

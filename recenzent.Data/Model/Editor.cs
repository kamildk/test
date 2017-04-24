using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Editor {
        public int EditorId { get; set; }

        //public string UserId { get; set; }
        //[ForeignKey("UserId")]
        //public User User { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } 

        public Editor() {
            Reviews = new List<Review>();
        }
    }
}

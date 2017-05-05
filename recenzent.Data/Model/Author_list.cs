using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class Author_list
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Author_listId { get; set; }

        //public int UserId { get; set; }
        //[ForeignKey("UserId")]
        //public User User { get; set; }

        //public int PublicationId { get; set; }
        //[ForeignKey("PublicationId")]
        //public Publication Publication { get; set; }
    }
}

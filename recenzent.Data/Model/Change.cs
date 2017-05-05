using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace recenzent.Data.Model
{
    public class Change
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChangeId { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Change_type Type { get; set; }

        //public int EmployeeId { get; set; }
        //public User Employee { get; set; }
        public File File { get; set; }

        public User User { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Employee {
        public int Id { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}

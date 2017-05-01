using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Employee_Job {
        public int Id { get; set; }

        public Job Job { get; set; }
        public Employee Employee { get; set; }
    }
}

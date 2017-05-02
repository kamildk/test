using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Job {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Employee_Job> EmployeeJobs { get; set; }
    }
}

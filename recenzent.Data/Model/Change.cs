using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class Change
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public Change_type Type { get; set; }

        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Owner { get; set; }

        //TODO: admin ID ? 
    }
}

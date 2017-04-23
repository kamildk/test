using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace recenzent.Data.Model
{
    public class Review
    {

        public int Id { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime Creation_date { get; set; }

        public DateTime Expiration_date { get; set; }

        public ICollection<File> Files { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Owner { get; set; }

        [ForeignKey("EditorId")]
        public Editor Editor { get; set; }

    }
}

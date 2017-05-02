using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Rating {
        public int Id { get; set; }
        [Required]
        [Range(1,5)]
        public int Value { get; set; }
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public Publication Publication { get; set; }
        [Required]
        public User User { get; set; }
    }
}

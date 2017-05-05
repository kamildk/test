using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Rating {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

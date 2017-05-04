using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class ReviewStateHistory {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime ChangeDate { get; set; }

        [Required]
        public Review Review { get; set; }
        [Required]
        public ReviewState ChangesToState { get; set; }
        [Required]
        public ReviewState ChangesFrom { get; set; }
    }
}

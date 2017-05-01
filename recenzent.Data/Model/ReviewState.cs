using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class ReviewState {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<ReviewStateHistory> ChangesFromHistory { get; set; }
        public ICollection<ReviewStateHistory> ChangesToHistory { get; set; }
    }
}

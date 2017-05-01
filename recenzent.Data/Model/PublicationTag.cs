using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class PublicationTag {
        public int Id { get; set; }

        [Required]
        public Tag Tag { get; set; }
        public int TagId { get; set; }

        [Required]
        public Publication Publication { get; set; }
        public int PublicationId { get; set; }
    }
}

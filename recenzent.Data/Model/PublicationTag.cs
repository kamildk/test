using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class PublicationTag {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("TagId")]
        public virtual Tag Tag { get; set; }
        public int TagId { get; set; }

        [Required]
        [ForeignKey("PublicationId")]
        public virtual Publication Publication { get; set; }
        public int PublicationId { get; set; }
    }
}

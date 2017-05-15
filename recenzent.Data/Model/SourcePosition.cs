using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class SourcePosition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SourcePositionId { get; set; }

        [Required]
        [ForeignKey("SourceId")]
        public Source Source { get; set; }
        public int SourceId { get; set; }

        [Required]
        [ForeignKey("PublicationId")]
        public Publication Publication { get; set; }
        public int PublicationId { get; set; }
        
    }
}

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
        public Source Source { get; set; }
        [Required]
        public Publication Publication { get; set; }
    }
}

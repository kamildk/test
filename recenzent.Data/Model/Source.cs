using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Source {
        public int SourceId { get; set; }
        public string Name { get; set; }

        public int SourcePositionId { get; set; }
        [ForeignKey("SourcePositionId")]
        public SourcePosition SourcePosition { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Sources {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("SourcePositionId")]
        public SourcePosition SourcePosition { get; set; }
    }
}

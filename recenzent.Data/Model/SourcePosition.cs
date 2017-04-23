using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class SourcePosition {
        public int Id { get; set; }

        public ICollection<Sources> Sources { get; set; }
    }
}

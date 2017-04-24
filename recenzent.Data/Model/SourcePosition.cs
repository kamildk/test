using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class SourcePosition
    {
        public int SourcePositionId { get; set; }

        public ICollection<Source> Sources { get; set; }

        //public ICollection<Publication> Publications { get; set; }
    }
}

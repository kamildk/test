using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class File
    {
        public int id { get; set; }

        public string link_source { get; set; }

        public bool isCurrent { get; set; }
    }
}

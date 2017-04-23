using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    class Publication
    {
        public int id { get; set; }

        public string title { get; set; }

        public int category_id { get; set; }

        public virtual Publication_category category { get; set; }

        public bool isShared { get; set; }

        public string abstact { get; set; }

    }
}

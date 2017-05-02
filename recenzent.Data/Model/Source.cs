﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Source {
        public int SourceId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<SourcePosition> SourcePositions { get; set; }
    }
}

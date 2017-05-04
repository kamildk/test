﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Affiliation {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AffiliationId { get; set; }
        [Required]
        public string Name { get; set; }

        //public ICollection<User> Users { get; set; }
    }
}

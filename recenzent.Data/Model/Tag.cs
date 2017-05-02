﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Tag {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<PublicationTag> PublicationTag { get; set; }
    }
}
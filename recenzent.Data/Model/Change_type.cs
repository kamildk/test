﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class Change_type
    {
        public int Change_typeId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Change> MyProperty { get; set; }
    }
}

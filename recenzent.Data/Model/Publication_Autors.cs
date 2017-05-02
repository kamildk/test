using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model {
    public class Publication_Autors {
        public int Id { get; set; }

        [Required]
        public Publication Publication { get; set; }
        [Required]
        public User Autor { get; set; }
    }
}

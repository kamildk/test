using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class Publication_category
    {
        public int Publication_categoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Publication> Publications { get; set; }
    }
}

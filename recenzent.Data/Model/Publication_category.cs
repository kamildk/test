using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class Publication_category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Publication_categoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }
    }
}

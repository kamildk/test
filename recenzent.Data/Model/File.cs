using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recenzent.Data.Model
{
    public class File
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FileId { get; set; }

        [Required]
        public string Link_source { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsCurrent { get; set; }

        public ICollection<Change> Changes { get; set; }
        public Review Review { get; set; }
        public Publication Publication { get; set; }
    }
}

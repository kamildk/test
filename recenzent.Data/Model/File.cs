using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace recenzent.Data.Model
{
    public class File
    {
        public int Id { get; set; }

        public string Link_source { get; set; }

        public bool IsCurrent { get; set; }

        public int ChangeId { get; set; }
        [ForeignKey("ChangeId")]
        public Change Change { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}

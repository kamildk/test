using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class Publication
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Category_id { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Publication_category Category { get; set; }

        public bool IsShared { get; set; }

        public string Abstact { get; set; }

        [ForeignKey("FilesId")]
        public ICollection<File> Files { get; set; }

        [ForeignKey("SourcePositionId")]
        public SourcePosition SourcePosition { get; set; }

        [ForeignKey("ReviewId")]
        public Review Review { get; set; }



    }
}

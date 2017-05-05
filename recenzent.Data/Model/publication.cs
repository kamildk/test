using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recenzent.Data.Model
{
    public class Publication
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PublicationId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public bool IsShared { get; set; }
        public DateTime ShareDate { get; set; }
        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Publication_category Category { get; set; }

        public User Author { get; set; }

        public ICollection<SourcePosition> SourcePositions { get; set; }
        public ICollection<File> Files { get; set; }
        public ICollection<PublicationTag> PublicationTags { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Comment> Comments { get; set; }
        //public ICollection<Publication_Autors> PublicationAutors { get; set; }

        //public int ReviewId { get; set; }
        //[ForeignKey("ReviewId")]
        //public Review Review { get; set; }
    }
}

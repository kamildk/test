﻿using System;
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
        public DateTime? ShareDate { get; set; }
        [Required]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Publication_category Category { get; set; }

        public string AuthoId { get; set; }
        [ForeignKey("AuthoId")]
        public virtual User Author { get; set; }

        //public ICollection<SourcePosition> SourcePositions { get; set; }
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<PublicationTag> PublicationTags { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }

        public Publication() {
            //SourcePositions = new List<SourcePosition>();
            Files = new List<File>();
            PublicationTags = new List<PublicationTag>();
            Ratings = new List<Rating>();
            Comments = new List<Comment>();
            Reviews = new List<Review>();
        }
        //public ICollection<Publication_Autors> PublicationAutors { get; set; }

        //public int ReviewId { get; set; }
        //[ForeignKey("ReviewId")]
        //public Review Review { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recenzent.Data.Model
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [Required]
        public DateTime Creation_date { get; set; }
        [Required]
        public DateTime Expiration_date { get; set; }

        public ICollection<ReviewStateHistory> StateHistory { get; set; }

        public ICollection<File> Files { get; set; }

        public int PublicationId { get; set; }
        [ForeignKey("PublicationId")]
        public Publication Publication { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int CurrentStateId { get; set; }
        [ForeignKey("CurrentStateId")]
        public ReviewState CurrentState { get; set; }

    }
}

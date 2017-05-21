using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using recenzent.Data;

namespace recenzent.Models {
    public class PublicationViewModel {

        public int Id { get; set; }
        public string Title { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Tags { get; set; }
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        // public string Sources { get; set; }
    }

    public class PublicationReviewListViewModel {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        //public List<string> Sources { get; set; }
        public List<PublicationReviewViewModel> Reviews { get; set; }

        public PublicationReviewListViewModel() {
            Tags = new List<string>();
            //Sources = new List<string>();
            Reviews = new List<PublicationReviewViewModel>();
        }
    }

    public class PublicationReviewViewModel {
        public int Id { get; set; }
        public DateTime AddDate { get; set; }
        public string State { get; set; }
    }

    public class PublicationEditViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Tags { get; set; }
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        public IEnumerable<Data.Model.Review> AllReviews {get; set;}

    }

}
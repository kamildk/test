using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using recenzent.Data;
using recenzent.Data.Model;

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
        public List<Review> AllReviews {get; set;}

    }

    public class PublicationHomeViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string AuthorName { get; set; }
        public string ShareDate { get; set; }
    }

    public class PublicationSmallViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class PublicationAdminListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string AuthorName { get; set; }
        public string ShareDate { get; set; }
        public bool isShared { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace recenzent.Models {
    public class PublicationViewModel {

        public string Title { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Tags { get; set; }
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        public string Sources { get; set; }
    }

    public class PublcationReviewViewModel {
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> Sources { get; set; }
        public List<recenzent.Data.Model.Review> Reviews { get; set; }

        public PublcationReviewViewModel() {
            Tags = new List<string>();
            Sources = new List<string>();
            Reviews = new List<Data.Model.Review>();
        }
    }
}
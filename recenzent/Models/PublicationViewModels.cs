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
        public List<string> Sources { get; set; }
    }
}
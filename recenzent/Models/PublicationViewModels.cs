using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recenzent.Models {
    public class PublicationViewModel {

        public string Title { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
    }
}
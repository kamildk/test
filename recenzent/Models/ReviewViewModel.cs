using recenzent.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace recenzent.Models
{
    public class ReviewViewModel
    {
        public HttpPostedFileBase File { get; set; }

        public int pubId{get; set; }
    }
}
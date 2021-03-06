﻿using recenzent.Data.Model;
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
        public int ReviewId { get; set; }
    }
    public class OrderReviewViewModel
    {
        public int pubId { get; set; }
        public int ReviewerId { get; set; }
        public List<User> ValidUsers{ get; set; }
        public string Message { get; set; }
        public int ValidUsersCount { get; set; }
        public bool[] Rewiever { get; set; }

    }

}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace recenzent.Controllers
{
    public class ReviewerController : Controller
    {
        // GET: Reviewer
        public ActionResult Index()
        {
            return View();
        }
    }
}
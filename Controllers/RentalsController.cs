﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaSharpAuth.Controllers
{
    public class RentalsController : Controller
    {
        // GET: Rentals/New
        public ActionResult New()
        {
            return View();
        }
    }
}
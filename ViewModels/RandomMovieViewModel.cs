using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaSharpAuth.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
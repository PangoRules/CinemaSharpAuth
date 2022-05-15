using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CinemaSharpAuth.ViewModels
{
    public class MovieFormViewModel
    {
        public Movie Movie { get; set; }
        public IEnumerable<Genre> Genre { get; set; }
    }
}
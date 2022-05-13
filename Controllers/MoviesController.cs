using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaSharpAuth.Controllers
{
    public class MoviesController : Controller
    {
        // GET : Movies
        /// <summary>
        /// Function that returns the view with the current movies
        /// </summary>
        /// <returns>ViewResult: Views/Movies/Index.cshtml</returns>
        public ViewResult Index()
        {
            var movies = GetMovies();

            return View(movies);
        }

        /// <summary>
        /// Current function to create movies list
        /// </summary>
        /// <returns>IEnumerable: Enumerable list with the current movies to show in table</returns>
        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie{Id = 1, Name = "Shrek 1"},
                new Movie{Id = 1, Name = "Shrek 2"},
            };
        }

        #region[PRUEBAS COMENTADAS]
        // GET: Movies/Random
        //public ViewResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek!" };
        //    var customers = new List<Customer>
        //    {
        //        new Customer{Name = "Customer 1"},
        //        new Customer{Name = "Customer 2"},
        //        new Customer{Name = "Customer 3"},
        //        new Customer{Name = "Customer 4"},
        //        new Customer{Name = "Customer 5"},
        //        new Customer{Name = "Customer 6"}
        //    };

        //    var viewModel = new RandomMovieViewModel
        //    {
        //        Movie = movie,
        //        Customers = customers
        //    };

        //    return View(viewModel);
        //}

        //// GET: Movies
        //public ActionResult Random()
        //{
        //    var movie = new Movie() { Name = "Shrek!" };

        //    //return View(movie);
        //    //return Content("Hello word");
        //    //return HttpNotFound();
        //    //return new EmptyResult();
        //    return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
        //}

        // GET: Movies/Edit
        //public ContentResult Edit(int id)
        //{
        //    return Content("id = " + id);
        //}

        //Get: Movies
        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    pageIndex = pageIndex.HasValue ? pageIndex : 1;
        //    sortBy = String.IsNullOrWhiteSpace(sortBy) ? "Name" : sortBy;

        //    return Content(String.Format("pageIndex = {0} & sortBy = {1}", pageIndex, sortBy));
        //}

        //[Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}

        #endregion
    }
}
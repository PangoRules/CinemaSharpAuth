using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaSharpAuth.Controllers
{
    public class MoviesController : Controller
    {
        /// <summary>
        /// Variable that helps query the database (MUST BE INITIALIZED IN CONSTRUCTOR)
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Constructor to initialize varaibles
        /// </summary>
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// This is a method of Controller class, in this case it is used to dispose de context from the database
        /// </summary>
        /// <param name="disposing">bool: Indicase whether or no t to dispose the _context variable</param>
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET : Movies
        /// <summary>
        /// Function that returns the view with the current movies
        /// </summary>
        /// <returns>ViewResult: Views/Movies/Index.cshtml</returns>
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(c => c.Genre).ToList();

            return View(movies);
        }

        //GET: Movies/details/{id}
        /// <summary>
        /// Function that returns the view with the selected movie according to it's id
        /// </summary>
        /// <param name="id">int: Movie identifier</param>
        /// <returns>View: -View/Movies/Details.cshtml</returns>
        [Route("movies/details/{id:int}")]
        public ViewResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).FirstOrDefault(c => c.Id == id);

            if(movie == null)
                throw new HttpException(404, "Not found");

            return View(movie);
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
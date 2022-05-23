using CinemaSharpAuth.Models;
using CinemaSharpAuth.ViewModels;
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
        #region[Variables, constructor and Dispose method]
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
        #endregion

        #region[Views]
        // GET : Movies
        /// <summary>
        /// Function that returns the view with the current movies
        /// </summary>
        /// <returns>ViewResult: Views/Movies/Index.cshtml</returns>
        public ViewResult Index()
        {
            return View();
        }

        //GET: Movies/details/{id}
        /// <summary>
        /// Function that returns the view with the selected movie according to it's id
        /// </summary>
        /// <param name="id">int: Movie identifier</param>
        /// <returns>View: -View/Movies/Details.cshtml</returns>
        [Route("Movies/Details/{id:int}")]
        public ViewResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).FirstOrDefault(c => c.Id == id);

            if(movie == null)
                throw new HttpException(404, "Not found");

            return View(movie);
        }

        //GET: Movies/New
        /// <summary>
        /// Function that returns the view with the form to create a new movie
        /// </summary>
        /// <returns>View: ~/Views/Movies/MovieForm.cshtml</returns>
        public ViewResult New()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new MovieFormViewModel
            {
                Genre = genres,
                Movie = new Movie()
            };

            return View("MovieForm", viewModel);
        }

        //GET: Movies/Edit
        /// <summary>
        /// Function that returns the view with the form to edit a current movie
        /// </summary>
        /// <param name="id">int: Identifier of the movie to edit.</param>
        /// <returns>View: ~/Views/Movies/MovieForm.cshtml</returns>
        [Route("Movies/Edit/{id:int}")]
        public ViewResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if(movie == null)
                throw new HttpException(404, "Not found");

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genre = _context.Genres.ToList()
            };

            return View("MovieForm", viewModel);
        }
        #endregion

        #region[Actions]
        //POST: Movies/Save
        /// <summary>
        /// Funciton that saves a new or updates an existing movie from the database
        /// </summary>
        /// <param name="movie">Movie: ViewModel with the data to add or update a movie in the database</param>
        /// <returns>RedirectToAction: MoviesControler.Index()</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            #region[Validations]
            if(!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genre = _context.Genres.ToList()
                };

                return View("MoviesForm", viewModel);
            }
            #endregion

            if(movie.Id == 0)
                _context.Movies.Add(movie);
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
        #endregion

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
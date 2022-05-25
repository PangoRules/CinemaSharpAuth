using CinemaSharpAuth.Dto;
using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemaSharpAuth.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        #region[Variables, Constructor and dispose function]
        /// <summary>
        /// Variable in charge of manipulating the database
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Constructor to initialize variables
        /// </summary>
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        /// <summary>
        /// Funciton that disposes the ApplicationDbContext
        /// </summary>
        /// <param name="disposing">bool: Boolean that indicates if it should be disposed (NOT IN USE)</param>
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        #endregion

        //POST: api/NewRentals
        /// <summary>
        /// Function that creates a new rental for a list of selected movies.
        /// </summary>
        /// <param name="newRental">NewRentalDto: Model with the data of the movies to rent.</param>
        /// <returns>IHttpActionResult: Message of the result of the added movies</returns>
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == newRental.CustomerId);

            var movies = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            #region[Validations]
            if(newRental.MovieIds.Count == 0)
                return BadRequest("No movieIds have been given.");

            if(customer == null)
                return BadRequest("CustomerId not valid.");

            if(movies.Count != newRental.MovieIds.Count)
                return BadRequest("One or more movieIds are invalid.");
            #endregion

            foreach(var movie in movies)
            {
                if(movie.NumberAvailable == 0)
                    return BadRequest("Movie: "+movie.Name+" is not available");

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return Ok("Rental created succesfully");
        }
    }
}

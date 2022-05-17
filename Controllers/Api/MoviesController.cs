using AutoMapper;
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
    public class MoviesController : ApiController
    {
        #region[Variables, Constructor and dispose function]
        /// <summary>
        /// Variable in charge of manipulating the database
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Constructor to initialize variables
        /// </summary>
        public MoviesController()
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

        //GET: api/movies
        /// <summary>
        /// Function that returns the list of movies on the server
        /// </summary>
        /// <returns>IHttpActionResult: List of movies</returns>
        public IHttpActionResult GetMovies()
        {
            IEnumerable<MovieDto> moviesList = _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(moviesList);
        }

        //GET: api/movies/{id}
        /// <summary>
        /// Function that returns a specific movie based on it's id
        /// </summary>
        /// <param name="id">int: id of the movie to query</param>
        /// <returns>IHttpActionResult: Movie queried form its id</returns>
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if(movie == null)
                return NotFound();

            var movieDto = Mapper.Map<Movie, MovieDto>(movie);

            return Ok(movieDto);
        }

        //POST: api/movies
        /// <summary>
        /// Function that creates a new movie
        /// </summary>
        /// <param name="newMovieDto">MovieDto: Model with the data of the new movie to create</param>
        /// <returns>IHttpActionResult: Movie model with the url to the new created movie</returns>
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto newMovieDto)
        {
            #region[Validating Model]
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            #endregion

            var movie = Mapper.Map<MovieDto, Movie>(newMovieDto);

            _context.Movies.Add(movie);

            _context.SaveChanges();

            newMovieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), newMovieDto);
        }

        //PUT: api/movies/{id}
        /// <summary>
        /// Function that updates a current movie based on it's id and data passed through body.
        /// </summary>
        /// <param name="id">int: id of the movie to update.</param>
        /// <param name="movieDto">MovieDto: Model with the new data of the movie.</param>
        /// <returns>IHttpActionResult: Ok message if movie was updated correctly.</returns>
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            #region[Validating Model]
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            #endregion

            var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);

            if(movieInDB == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDB);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE: api/movies/{id}
        /// <summary>
        /// Function that deletes a movie based on it's id.
        /// </summary>
        /// <param name="id">int: id of the movie to delete.</param>
        /// <returns>IHttpActionResult: Ok message if movie deleted succesfully</returns>
        public IHttpActionResult DeleteMovie(int id)
        {
            #region[Validating Model]
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            #endregion

            var movieInDB = _context.Movies.SingleOrDefault(c => c.Id == id);

            if(movieInDB == null)
                return NotFound();

            _context.Movies.Remove(movieInDB);

            _context.SaveChanges();

            return Ok();
        }
    }
}

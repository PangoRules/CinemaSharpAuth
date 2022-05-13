using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaSharpAuth.Controllers
{
    public class CustomersController : Controller
    {
        /// <summary>
        /// Variable that helps query the database (MUST BE INITIALIZED IN CONSTRUCTOR)
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Constructor to initialize varaibles
        /// </summary>
        public CustomersController()
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

        /// GET: Customers
        /// <summary>
        /// Function in charge of returning the view with the list of the
        /// customers on a table
        /// </summary>
        /// <returns>ViewResult: Customers/Index.cshtml</returns>
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        /// GET: customers/details/{id}
        /// <summary>
        /// Function that returns a view with the details of the selected customer
        /// </summary>
        /// <param name="id">Int: Identifier of the selected user</param>
        /// <returns>ViewResult: </returns>
        [Route("customers/details/{id:int}")]
        public ViewResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if(customer == null)
                throw new HttpException(404, "Not found");

            return View(customer);
        }
    }
}
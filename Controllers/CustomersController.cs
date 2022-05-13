using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaSharpAuth.Controllers
{
    public class CustomersController : Controller
    {
         /// GET: Customers
        /// <summary>
        /// Function in charge of returning the view with the list of the
        /// customers on a table
        /// </summary>
        /// <returns>ViewResult: Customers/Index.cshtml</returns>
        public ViewResult Index()
        {
            var customers = GetCustomers();

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
            var customer = GetCustomers().SingleOrDefault(c => c.Id == id);

            if(customer == null)
                throw new HttpException(404, "Not found");

            return View(customer);
        }

        /// <summary>
        /// Function that gets the list of customers to show in table
        /// </summary>
        /// <returns>List<Customer>: List of customers</Customer></returns>
        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                new Customer{Id = 1, Name = "Alex Alegria"},
                new Customer{Id = 2, Name = "Kim Alegria"},
                new Customer{Id = 3, Name = "Yessica Cameras"},
            };
        }
    }
}
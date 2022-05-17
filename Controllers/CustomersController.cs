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
    public class CustomersController : Controller
    {
        #region[Variables, dispose and constructor]

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

        #endregion


        #region[Views]
        // GET: Customers
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

        // GET: Customers/Details/{id}
        /// <summary>
        /// Function that returns a view with the details of the selected customer
        /// </summary>
        /// <param name="id">Int: Identifier of the selected user</param>
        /// <returns>ViewResult: ~/Views/Customers/Details.cshtml</returns>
        [Route("Customers/Details/{id:int}")]
        public ViewResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if(customer == null)
                throw new HttpException(404, "Not found");

            return View(customer);
        }

        // GET: Customers/New
        /// <summary>
        /// Function that returns a view with the form to create a new customer
        /// </summary>
        /// <returns>View: ~Views/Customers/CustomerForm.cshtml</returns>
        public ViewResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = new Customer()
            };

            return View("CustomerForm", viewModel);
        }

        //GET: Customers/Edit
        /// <summary>
        /// Function that returns a view to edit a customers information
        /// </summary>
        /// <param name="id">int: Identifier of the customer to edit</param>
        /// <returns>View: ~Views/Customers/CustomerForm.cshtml</returns>
        [Route("Customers/Edit/{id:int}")]
        public ViewResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
                throw new HttpException(404, "Not found");

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
        #endregion

        #region[Actions]
        //POST: Customers/Save
        /// <summary>
        /// Funciton that saves a new customer to the database and redirects to list of customers
        /// </summary>
        /// <param name="customer">Customer: Model with the data of the new customer</param>
        /// <returns>RedirectToRouteResult: Redirects to Index()</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            #region[Validating Model]
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }
            #endregion

            if(customer.Id == 0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipType = customer.MembershipType;
                customerInDb.IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        #endregion

    }
}
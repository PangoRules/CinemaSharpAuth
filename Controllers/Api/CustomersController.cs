using AutoMapper;
using CinemaSharpAuth.Dto;
using CinemaSharpAuth.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CinemaSharpAuth.Controllers.Api
{
    public class CustomersController : ApiController
    {
        #region[Variables, Constructor and dispose function]
        /// <summary>
        /// Variable in charge of manipulating the database
        /// </summary>
        private ApplicationDbContext _context;

        /// <summary>
        /// Constructor to initialize variables
        /// </summary>
        public CustomersController()
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

        //GET: api/customers
        /// <summary>
        /// Function that gets the list of current customers in the database
        /// </summary>
        /// <returns>IHttpActionResult: List of customers</returns>
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers.Include(c => c.MembershipType);

            if(!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

            var customersList = customersQuery.ToList().Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customersList);
        }

        //GET: api/customers/{id}
        /// <summary>
        /// Function that returns the data of a current customer from it's id.
        /// </summary>
        /// <param name="id">int: Identifier of the customer to query from the database.</param>
        /// <returns>IHttpActionResult: Object with the data of the selected customer</returns>
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customer == null)
                return NotFound();

            var customerDto = Mapper.Map<Customer, CustomerDto>(customer);

            return Ok(customerDto);
        }

        //POST: api/customers
        /// <summary>
        /// Function that creates a new customer
        /// </summary>
        /// <param name="newCustomer">CustomerDto: Model with the data of the customer to add</param>
        /// <returns>IHttpActionResult: CustomerDto object with the new added customer</returns>
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto newCustomer)
        {
            #region[Validating Model]
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            #endregion

            var customer = Mapper.Map<CustomerDto, Customer>(newCustomer);

            _context.Customers.Add(customer);

            _context.SaveChanges();

            newCustomer.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), newCustomer);
        }

        //PUT api/customer/{id}
        /// <summary>
        /// Function that updates a Customer according to the data passed in the model and it's id
        /// </summary>
        /// <param name="id">int: id of the customer to update.</param>
        /// <param name="customer">Customer: Model with the data of the customer to edit.</param>
        /// <returns>HttpStatusCode: Code that indicates if everything went OK.</returns>
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customer)
        {
            #region[Validating Model]
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            #endregion

            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDB == null)
                return NotFound();

            Mapper.Map(customer, customerInDB);

            _context.SaveChanges();

            return Ok();
        }

        //DELETE: api/customer/{id}
        /// <summary>
        /// Function that deletes a current customer from the database according to it's id.
        /// </summary>
        /// <param name="id">int: id of the customer to delete from the database.</param>
        /// <returns>IHttpActionResult: Ok message.</returns>
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
           #region[Validating Model]
            if(!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            #endregion

            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(customerInDB == null)
                return NotFound();

            _context.Customers.Remove(customerInDB);

            _context.SaveChanges();

            return Ok();
        }
    }
}

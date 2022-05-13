using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CinemaSharpAuth.Models
{
    public class CinemaSharpAuthContext : DbContext
    {
        public CinemaSharpAuthContext() { }
        //Entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
    }
}
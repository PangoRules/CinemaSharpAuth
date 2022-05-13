using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CinemaSharpAuth.Models
{
    public class Genre
    {
        public byte Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SMS.Models
{
    public class Cart
    {
        [Key]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public User User { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}




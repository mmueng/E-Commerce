using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace E_Commerce.Models {
    public class Customer {

        [Key]
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Orders> OrdersCustomer { get; set; }

    }
}
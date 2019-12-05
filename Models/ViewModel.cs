using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models {
    public class ViewModel {
        // User Model
        public Product NewProduct { get; set; }
        public List<Product> AllProducts { get; set; }

        public Customer NewCustomer { get; set; }
        public List<Customer> AllCustomers { get; set; }
        // Order Model
        public Orders NewOrder { get; set; }
        public List<Orders> AllOrders { get; set; }

    }
}
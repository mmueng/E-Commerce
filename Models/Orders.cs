using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models {

    public class Orders {
        [Key]
        public int OrderId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CustomerId { get; set; }

        [Range (0, 1000)]
        public int SoldQty { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // public Product ProductOrder { get; set; }
        public Customer Customer { get; set; }

    }
}
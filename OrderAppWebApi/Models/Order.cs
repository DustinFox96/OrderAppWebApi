using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderAppWebApi.Models {
    public class Order {
        public int Id { get; set; }
        [StringLength(200), Required]
        public string Description { get; set; }
        [StringLength(12), Required]
        public string Status { get; set; } = "NEW";
        [Column(TypeName = "decimal (9,2)")]
        public decimal Total { get; set; }
        public int CustomerId { get; set; }
        public virtual  Customers customer  { get; set; } // this is saying it's going to be in our class but not our database


        public Order() { }
    }
}

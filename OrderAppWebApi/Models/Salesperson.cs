using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAppWebApi.Models {
    public class Salesperson {
        public int Id { get; set; }
        [StringLength(50), Required]
        public string Name { get; set; }
        [StringLength(2)]
        public string StateCode { get; set; }
        [Column(TypeName = "decimal (9,2)"), Required]
        public decimal Sales { get; set; }

        public Salesperson() { }
    }
}


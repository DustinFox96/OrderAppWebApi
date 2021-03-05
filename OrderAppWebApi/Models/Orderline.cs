using OrderAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EOrderAppWebApi.Controllers {
    public class Orderline {
        public int Id { get; set; }
        [Column (TypeName = "int"), Required] // I did not have to do this, only strings or decs need it, everything else is already nonnullable as with all the rest down below.
        public int OrderId { get; set; }
        [Column(TypeName = "int"), Required]
        public int ItemId { get; set; }
        [Column (TypeName = "int"), Required ]
        public int Quantity { get; set; }
        public virtual Order order { get; set; }
        public virtual Item item { get; set; }


        public Orderline() { }
    }
}

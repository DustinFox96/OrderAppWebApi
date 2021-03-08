using OrderAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace OrderAppWebApi.Models {
    public class Orderline {
        public int Id { get; set; }
        [Column (TypeName = "int"), Required] 
        public int OrderId { get; set; }
        [Column(TypeName = "int"), Required]
        public int ItemId { get; set; }
        [Column (TypeName = "int"), Required ]
        public int Quantity { get; set; }
        [JsonIgnore] // this will ignore the order as a foreign key when getting info., this stops the order appearing it's instance in postman
        public virtual Order Order { get; set; }
        public virtual Item Item { get; set; }


        public Orderline() { }
    }
}

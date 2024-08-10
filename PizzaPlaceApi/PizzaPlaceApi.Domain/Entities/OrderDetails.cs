using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.Entities
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; } 
        public int OrderId { get; set; }
        public string PizzaId { get; set; }
        public int Quantity { get; set; }

        //Navigation Property
        public Orders Order { get; set; }
        public Pizza Pizza { get; set; }    
    }
}

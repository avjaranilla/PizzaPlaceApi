using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.DTOs
{
    public class OrderDetailsDTO
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public string PizzaId { get; set; }
        public int Quantity { get; set; }

        public string PizzaName { get; set; }
        public string PizzaSize { get; set; }
    }
}

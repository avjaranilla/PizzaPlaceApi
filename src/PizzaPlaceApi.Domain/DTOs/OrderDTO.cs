using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public IEnumerable<OrderDetailsDTO> OrderDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.DTOs
{
    public class UpdateOrderDTO
    {
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }  // Combined Date and Time
        public List<UpdateOrderDetailsDTO> OrderDetails { get; set; }
    }
}

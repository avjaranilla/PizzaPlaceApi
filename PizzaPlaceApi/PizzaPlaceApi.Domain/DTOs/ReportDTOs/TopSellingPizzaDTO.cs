using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.DTOs.ReportDTOs
{
    public class TopSellingPizzaDTO
    {
        public string PizzaName { get; set; }
        public int TotalQuantity { get; set; }
    }
}

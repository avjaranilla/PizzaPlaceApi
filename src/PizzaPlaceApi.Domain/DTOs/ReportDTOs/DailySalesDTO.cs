using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.DTOs.ReportDTOs
{
    public class DailySalesDTO
    {
        public DateOnly Date { get; set; }
        public decimal TotalSales { get; set; }

    }
}

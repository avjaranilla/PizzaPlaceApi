using CsvHelper.Configuration;
using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Mapping
{
    public class OrderDetailsMap : ClassMap<OrderDetails>
    {
        public OrderDetailsMap()
        {
            Map(m => m.OrderDetailsId).Name("order_details_id");
            Map(m => m.OrderId).Name("order_id");
            Map(m => m.PizzaId).Name("pizza_id");
            Map(m => m.Quantity).Name("quantity");
        }
    }
}

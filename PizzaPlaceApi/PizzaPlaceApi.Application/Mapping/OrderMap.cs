using CsvHelper.Configuration;
using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Mapping
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap() 
        {
            Map(m => m.OrderId).Name("order_id");
            Map(m => m.Date).Name("date");
            Map(m => m.Time).Name("time");
        }
    }
}

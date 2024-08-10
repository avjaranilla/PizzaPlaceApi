﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.Entities
{
    public class Orders
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }


        //Navigation Property
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}

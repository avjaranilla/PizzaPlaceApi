using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.Entities
{
    public class Pizza
    {
        public string PizzaId { get; set; }
        public string PizzaTypeId { get; set; }
        public char Size { get; set; }
        public decimal Price { get; set; }


        //Navigation Property
        public PizzaType PizzaType { get; set; }
    }
}

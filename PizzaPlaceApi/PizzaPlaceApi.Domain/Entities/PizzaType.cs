using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.Entities
{
    public class PizzaType
    {
        public string PizzaTypeId { get; set; }
        public string Name {  get; set; }
        public string Category { get; set; }
        public string Ingredients { get; set; }


        //Navigation Property
        public ICollection<Pizza> Pizzas { get; set; }

    }
}

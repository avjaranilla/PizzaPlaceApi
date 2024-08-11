using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Interfaces
{
    public interface IPizzaService
    {
        Task<IEnumerable<Pizza>> GetAllPizzasAsync();
        Task<Pizza> GetPizzaByIdAsync(string id);
        Task AddPizzaAsync(Pizza pizza);
        Task <Pizza> UpdatePizzaAsync(Pizza pizza);
        Task DeletePizzaAsync(string id);
    }
}

using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Interfaces
{
    public interface IPizzaTypeService
    {
        Task<IEnumerable<PizzaType>> GetAllPizzaTypesAsync();
        Task<PizzaType> GetPizzaTypeByIdAsync(string id);
        Task AddPizzaTypeAsync(PizzaType pizzaType);
        Task<PizzaType> UpdatePizzaTypeAsync(PizzaType pizzaType);
        Task DeletePizzaTypeAsync(string id);
    }
}

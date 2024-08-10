using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.Repositories
{
    public interface IPizzaTypeRepository
    {
        Task<IEnumerable<PizzaType>> GetAllAsync();
        Task<PizzaType> GetByIdAsync(string id);
        Task AddAsync(PizzaType pizzaType);
        Task UpdateAsync(PizzaType pizzaType);
        Task DeleteAsync(string id);
    }
}

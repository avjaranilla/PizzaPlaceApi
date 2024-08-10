using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.Repositories
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<Pizza>> GetAllAsync();
        Task<Pizza> GetByIdAsync(string id);
        Task AddAsync(Pizza pizza);
        Task UpdateAsync(Pizza pizza);
        Task DeleteAsync(string id);
    }
}

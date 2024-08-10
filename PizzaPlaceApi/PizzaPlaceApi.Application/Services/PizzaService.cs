using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Domain.Entities;
using PizzaPlaceApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Services
{

    public class PizzaService : IPizzaService
    {
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(IPizzaRepository pizzaRepository)
        {
            _pizzaRepository = pizzaRepository;
        }

        public async Task AddPizzaAsync(Pizza pizza)
        {
            await _pizzaRepository.AddAsync(pizza);
        }

        public async Task DeletePizzaAsync(string id)
        {
            await _pizzaRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Pizza>> GetAllPizzasAsync()
        {
            return await _pizzaRepository.GetAllAsync();
        }

        public async Task<Pizza> GetPizzaByIdAsync(string id)
        {
            return await _pizzaRepository.GetByIdAsync(id);    
        }

        public async Task UpdatePizzaAsync(Pizza pizza)
        {
            await _pizzaRepository.UpdateAsync(pizza);
        }
    }
}

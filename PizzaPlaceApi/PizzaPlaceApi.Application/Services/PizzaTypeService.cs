using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Domain.Entities;
using PizzaPlaceApi.Domain.Repositories;
using PizzaPlaceApi.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Services
{
    public class PizzaTypeService : IPizzaTypeService
    {
        private readonly IPizzaTypeRepository _pizzaTypeRepository;

        public PizzaTypeService(IPizzaTypeRepository pizzaTypeRepository)
        {
            _pizzaTypeRepository = pizzaTypeRepository;
        }

        public async Task AddPizzaTypeAsync(PizzaType pizzaType)
        {
            await _pizzaTypeRepository.AddAsync(pizzaType);
        }

        public async Task DeletePizzaTypeAsync(string id)
        {
            await _pizzaTypeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PizzaType>> GetAllPizzaTypesAsync()
        {
            return await _pizzaTypeRepository.GetAllAsync();
        }

        public async Task<PizzaType> GetPizzaTypeByIdAsync(string id)
        {
            return await _pizzaTypeRepository.GetByIdAsync(id);
        }

        public async Task<PizzaType> UpdatePizzaTypeAsync(PizzaType pizzaType)
        {
             // Fetch the existing pizza type from the repository
            var existingPizzaType = await _pizzaTypeRepository.GetByIdAsync(pizzaType.PizzaTypeId);

            if (existingPizzaType == null)
            {
                throw new Exception($"{pizzaType.PizzaTypeId} Not Found.");
            }

            // Update the existing entity's properties
            existingPizzaType.Category = pizzaType.Category;
            existingPizzaType.Ingredients = pizzaType.Ingredients;
            existingPizzaType.Name = pizzaType.Name;

            // Update the entity in the repository
            await _pizzaTypeRepository.UpdateAsync(existingPizzaType);

            return existingPizzaType; // Return the updated pizza type
        }
    }
}

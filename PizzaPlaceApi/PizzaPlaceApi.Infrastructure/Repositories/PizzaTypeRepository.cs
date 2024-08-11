using Microsoft.EntityFrameworkCore;
using PizzaPlaceApi.Domain.Entities;
using PizzaPlaceApi.Domain.Repositories;
using PizzaPlaceApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Infrastructure.Repositories
{
    public class PizzaTypeRepository : IPizzaTypeRepository
    {
        private readonly AppDbContext _context;

        public PizzaTypeRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task AddAsync(PizzaType pizzaType)
        {
            _context.PizzaTypes.Add(pizzaType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var pizzaType = await _context.PizzaTypes.FindAsync(id);
            if (pizzaType != null)
            {
                _context.PizzaTypes.Remove(pizzaType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PizzaType>> GetAllAsync()
        {
            return await _context.PizzaTypes.ToListAsync();
        }

        public async Task<PizzaType> GetByIdAsync(string id)
        {
            return await _context.PizzaTypes.FindAsync(id);
        }

        public async Task UpdateAsync(PizzaType pizzaType)
        {
            _context.PizzaTypes.Update(pizzaType);
            await _context.SaveChangesAsync();
        }

        public async Task<List<PizzaType>> GetPizzaTypesByIdAsync(List<string> pizzaTypeIds)
        {
            return await _context.PizzaTypes
           .Where(p => pizzaTypeIds.Contains(p.PizzaTypeId))
           .ToListAsync();
        }
    }
}

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
    public class PizzaRepository : IPizzaRepository
    {
        private readonly AppDbContext _context;

        public PizzaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza != null)
            {
                _context.Pizzas.Remove(pizza);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Pizza>> GetAllAsync()
        {
            return await _context.Pizzas.ToListAsync();
        }

        public async Task<Pizza> GetByIdAsync(string id)
        {
            return await _context.Pizzas.FindAsync(id);
        }

        public async Task UpdateAsync(Pizza pizza)
        {
            _context.Pizzas.Update(pizza);
            await _context.SaveChangesAsync();
        }
    }
}

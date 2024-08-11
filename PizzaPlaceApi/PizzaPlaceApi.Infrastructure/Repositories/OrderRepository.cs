using Microsoft.EntityFrameworkCore;
using PizzaPlaceApi.Domain.DTOs;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task CreateOrderDetailsAsync(List<OrderDetails> orderDetails)
        {
            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDTO>> GetAllOrdersAsync(int pageNumber, int pageSize)
        {
            var orders = await _context.Orders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                   .Select(o => new OrderDTO
                   {
                       OrderId = o.OrderId,
                       Date = o.Date,
                       Time = o.Time,
                       OrderDetails = _context.OrderDetails
                           .Where(od => od.OrderId == o.OrderId)
                           .Join(_context.Pizzas,
                               od => od.PizzaId,
                               p => p.PizzaId,
                               (od, p) => new { od, p })
                           .Join(_context.PizzaTypes,
                               combined => combined.p.PizzaTypeId,
                               pt => pt.PizzaTypeId,
                               (combined, pt) => new OrderDetailsDTO
                               {
                                   OrderDetailsId = combined.od.OrderDetailsId,
                                   OrderId = combined.od.OrderId,
                                   PizzaId = combined.od.PizzaId,
                                   Quantity = combined.od.Quantity,
                                   PizzaName = pt.Name,
                                   PizzaSize = combined.p.Size
                               })
                           .ToList()
                   }).ToListAsync();
            return orders;

        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Where(o => o.OrderId == orderId)
                 .Select(o => new OrderDTO
                 {
                     OrderId = o.OrderId,
                     Date = o.Date,
                     Time = o.Time,
                     OrderDetails = _context.OrderDetails
                           .Where(od => od.OrderId == o.OrderId)
                           .Join(_context.Pizzas,
                               od => od.PizzaId,
                               p => p.PizzaId,
                               (od, p) => new { od, p })
                           .Join(_context.PizzaTypes,
                               combined => combined.p.PizzaTypeId,
                               pt => pt.PizzaTypeId,
                               (combined, pt) => new OrderDetailsDTO
                               {
                                   OrderDetailsId = combined.od.OrderDetailsId,
                                   OrderId = combined.od.OrderId,
                                   PizzaId = combined.od.PizzaId,
                                   Quantity = combined.od.Quantity,
                                   PizzaName = pt.Name,
                                   PizzaSize = combined.p.Size
                               })
                           .ToList()
                 }).FirstOrDefaultAsync();

            return order;


        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOrderDetailsAsync(OrderDetails orderDetails)
        {
            _context.OrderDetails.RemoveRange(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrderDetailsAsync(OrderDetails orderDetails)
        {
            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .ToListAsync();
        }

        public async Task UpdateOrderDetailsAsync(OrderDetails orderDetails)
        {
            _context.OrderDetails.Update(orderDetails);
            await _context.SaveChangesAsync();
        }
    }
}

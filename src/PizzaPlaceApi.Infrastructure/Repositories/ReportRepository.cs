using Microsoft.EntityFrameworkCore;
using PizzaPlaceApi.Domain.DTOs.ReportDTOs;
using PizzaPlaceApi.Domain.Repositories;
using PizzaPlaceApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DailySalesDTO>> GetSalesReportByDateRangeAsync(DateTime startDate, DateTime endDate)
        {

            // Query to get the daily sales report by date range
            var _startDate = DateOnly.FromDateTime(startDate);
            var _endDate = DateOnly.FromDateTime(endDate);

            var report = await (from o in _context.Orders
                                join od in _context.OrderDetails
                                    on o.OrderId equals od.OrderId
                                join p in _context.Pizzas
                                    on od.PizzaId equals p.PizzaId
                                where o.Date >= _startDate && o.Date <= _endDate
                                group new { o.Date, od.Quantity, p.Price } by o.Date into g
                                select new DailySalesDTO
                                {
                                    Date = g.Key,
                                    TotalSales = g.Sum(x => x.Quantity * x.Price)
                                })
                                .OrderBy(d => d.Date)
                                .ToListAsync();

            return report;
        }

        public async Task<List<TopSellingPizzaDTO>> GetTopSellingPizzasAsync(int topN)
        {
            var topSellingPizzas = await _context.OrderDetails
            .Join(
                _context.Pizzas,
                orderDetail => orderDetail.PizzaId,
                pizza => pizza.PizzaId,
                (orderDetail, pizza) => new { orderDetail, pizza }
            )
            .Join(
                _context.PizzaTypes,
                combined => combined.pizza.PizzaTypeId,
                pizzaType => pizzaType.PizzaTypeId,
                (combined, pizzaType) => new { combined.orderDetail, pizzaType.Name }
            )
            .GroupBy(g => g.Name)
            .Select(group => new TopSellingPizzaDTO
            {
                PizzaName = group.Key,
                TotalQuantity = group.Sum(g => g.orderDetail.Quantity)
            })
            .OrderByDescending(dto => dto.TotalQuantity)
            .Take(topN)
            .ToListAsync();

            return topSellingPizzas;
        }
    }
}

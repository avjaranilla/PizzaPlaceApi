using CsvHelper;
using CsvHelper.Configuration;
using PizzaPlaceApi.Application.Interfraces;
using PizzaPlaceApi.Application.Mapping;
using PizzaPlaceApi.Domain.Entities;
using PizzaPlaceApi.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Services
{
    public class CsvImportService : ICsvImportService
    {
        private readonly AppDbContext _context;

        public CsvImportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ImportOrderDetailsDataAsync(string csvFilePath)
        {
            var orderDetails = ReadCsvFile<OrderDetails>(csvFilePath);
            _context.OrderDetails.AddRange(orderDetails);
            await _context.SaveChangesAsync();
        }

        public async Task ImportOrdersDataAsync(string csvFilePath)
        {
            var orders = ReadCsvFile<Order>(csvFilePath);
            _context.Orders.AddRange(orders);
            await _context.SaveChangesAsync();
        }

        public async Task ImportPizzaDataAsync(string csvFilePath)
        {
            var pizzas = ReadCsvFile<Pizza>(csvFilePath);
            _context.Pizzas.AddRange(pizzas);
            await _context.SaveChangesAsync();
        }

        public async Task ImportPizzaTypeDataAsync(string csvFilePath)
        {
            var pizzaTypes = ReadCsvFile<PizzaType>(csvFilePath);
            _context.PizzaTypes.AddRange(pizzaTypes);
            await _context.SaveChangesAsync();
        }

        private IEnumerable<T> ReadCsvFile<T>(string filePath) where T : class
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            // Register mappings based on the type of T
            if (typeof(T) == typeof(Pizza))
            {
                csv.Context.RegisterClassMap<PizzaMap>();
            }
            else if (typeof(T) == typeof(PizzaType))
            {
                csv.Context.RegisterClassMap<PizzaTypeMap>();
            }
            else if (typeof(T) == typeof(Order))
            {
                csv.Context.RegisterClassMap<OrderMap>();
            }
            else if (typeof(T) == typeof(OrderDetails))
            {
                csv.Context.RegisterClassMap<OrderDetailsMap>();
            }

            return csv.GetRecords<T>().ToList();
        }
    }
}

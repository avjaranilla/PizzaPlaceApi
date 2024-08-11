using CsvHelper;
using CsvHelper.Configuration;
using PizzaPlaceApi.Application.Interfaces;
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

        public async Task<int> ImportOrderDetailsDataAsync(string csvFilePath)
        {
            var orderDetails = ReadCsvFile<OrderDetails>(csvFilePath);
            _context.OrderDetails.AddRange(orderDetails);
            var count = await _context.SaveChangesAsync();
            return count;
        }

        public async Task<int> ImportOrdersDataAsync(string csvFilePath)
        {
            var orders = ReadCsvFile<Order>(csvFilePath);
            _context.Orders.AddRange(orders);
            var count = await _context.SaveChangesAsync();
            return count;
        }

        public async Task<int> ImportPizzaDataAsync(string csvFilePath)
        {
            // Read the pizzas from the CSV file
            var pizzas = ReadCsvFile<Pizza>(csvFilePath);

            // Add the pizzas to the context
            _context.Pizzas.AddRange(pizzas);

            // Save changes to the database
            var count = await _context.SaveChangesAsync();

            // Return the number of pizzas added
            return count;
        }

        public async Task<int> ImportPizzaTypeDataAsync(string csvFilePath)
        {
            var pizzaTypes = ReadCsvFile<PizzaType>(csvFilePath);
            _context.PizzaTypes.AddRange(pizzaTypes);
            var count = await _context.SaveChangesAsync();
            return count;
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

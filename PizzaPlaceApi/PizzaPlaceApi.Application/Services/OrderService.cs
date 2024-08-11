using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Domain.DTOs;
using PizzaPlaceApi.Domain.Entities;
using PizzaPlaceApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPizzaRepository _pizzaRepository;

        public OrderService(IOrderRepository orderRepository, IPizzaRepository pizzaRepository)
        {
            _orderRepository = orderRepository;
            _pizzaRepository = pizzaRepository;
        }

        public async Task CreateOrderAsync(CreateOrderDTO createOrderDto)
        {
            // Validate PizzaIds
            var pizzaIds = createOrderDto.OrderDetails.Select(od => od.PizzaId).Distinct().ToList();
            var validPizzas = await _pizzaRepository.GetPizzasByIdsAsync(pizzaIds);

            if (pizzaIds.Count != validPizzas.Count)
            {
                throw new ArgumentException("One or more PizzaIds are invalid.");
            }

            // Create Order entity
            var order = new Order
            {
                Date = DateOnly.FromDateTime(createOrderDto.Date),
                Time = TimeOnly.FromTimeSpan(createOrderDto.Date.TimeOfDay)
            };

            // Create OrderDetails entities
            var orderDetails = createOrderDto.OrderDetails.Select(od => new OrderDetails
            {
                OrderId = order.OrderId, // Initially set to zero, will be set after order creation
                PizzaId = od.PizzaId,
                Quantity = od.Quantity
            }).ToList();

            // Save Order
            await _orderRepository.CreateOrderAsync(order);

            // Set the OrderId for each OrderDetails
            foreach (var detail in orderDetails)
            {
                detail.OrderId = order.OrderId;
            }

            // Save OrderDetails
            await _orderRepository.CreateOrderDetailsAsync(orderDetails);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync(int pageNumber, int pageSize)
        {
            var orders = await _orderRepository.GetAllOrdersAsync(pageNumber, pageSize);
            return orders;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return order;
        }
    }
}

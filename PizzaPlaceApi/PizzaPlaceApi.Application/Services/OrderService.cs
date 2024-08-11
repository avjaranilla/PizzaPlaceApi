using PizzaPlaceApi.Application.Interfaces;
using PizzaPlaceApi.Domain.DTOs;
using PizzaPlaceApi.Domain.Entities;
using PizzaPlaceApi.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<OrderDTO> UpdateOrderAsync(UpdateOrderDTO updateOrderDto)
        {
            // Fetch the existing order
            var existingOrder = await _orderRepository.GetOrderByIdAsync(updateOrderDto.OrderId);
            if (existingOrder == null)
            {
                throw new ArgumentException($"Order with ID {updateOrderDto.OrderId} not found.");
            }

            // Update order properties
            existingOrder.Date = DateOnly.FromDateTime(updateOrderDto.OrderDateTime);
            existingOrder.Time = TimeOnly.FromTimeSpan(updateOrderDto.OrderDateTime.TimeOfDay);



            // Convert DTOs to entities
            var newOrderDetails = updateOrderDto.OrderDetails.Select(dto => new OrderDetails
            {
                OrderDetailsId = dto.OrderDetailsId,
                OrderId = updateOrderDto.OrderId,
                PizzaId = dto.PizzaId,
                Quantity = dto.Quantity
            }).ToList();


            // Find details to remove
            //var existingOrderDetails = await _orderRepository.GetOrderDetailsByOrderIdAsync(updateOrderDto.OrderId);
            var existingOrderDetails = existingOrder.OrderDetails;
            var existingOrderDetailsDict = existingOrderDetails.ToDictionary(od => od.OrderDetailsId);
            var newOrderDetailsDict = newOrderDetails.ToDictionary(od => od.OrderDetailsId);

            var detailsToRemove = existingOrderDetailsDict
                .Where(od => !newOrderDetailsDict.ContainsKey(od.Key))
                .Select(od => od.Value)
                .ToList();

            

            // Remove existing details
            foreach (var detail in detailsToRemove)
            {
                var detailsTorRemoveMappedToEntity = MapToOrderDetailsEntity(detail);
                await _orderRepository.RemoveOrderDetailsAsync(detailsTorRemoveMappedToEntity);
            }

            // Add new or updated details
            foreach (var detail in newOrderDetails)
            {
                if (!existingOrderDetailsDict.ContainsKey(detail.OrderDetailsId))
                {
                    await _orderRepository.AddOrderDetailsAsync(detail);
                }
                else
                {
                    await _orderRepository.UpdateOrderDetailsAsync(detail);
                }
            }

            // Update the order
            var mappedOrderEntity = MapToOrderEntity(existingOrder);
            await _orderRepository.UpdateOrderAsync(mappedOrderEntity);

            return new OrderDTO
            {
                OrderId = existingOrder.OrderId,
                Date = existingOrder.Date,
                Time = existingOrder.Time,
                OrderDetails = newOrderDetails.Select(od => new OrderDetailsDTO
                {
                    OrderDetailsId = od.OrderDetailsId,
                    OrderId = od.OrderId,
                    PizzaId = od.PizzaId,
                    Quantity = od.Quantity
                }).ToList()
            };
        }

        private Order MapToOrderEntity(OrderDTO orderDto)
        {
            return new Order
            {
                OrderId = orderDto.OrderId,
                Date = orderDto.Date,
                Time = orderDto.Time,
            };
        }

        private OrderDetails MapToOrderDetailsEntity(OrderDetailsDTO orderDetailsDto)
        {
            return new OrderDetails
            {
                OrderId = orderDetailsDto.OrderId,
                OrderDetailsId = orderDetailsDto.OrderDetailsId,
                PizzaId = orderDetailsDto.PizzaId,
                Quantity = orderDetailsDto.Quantity

            };
        }

    }
}

using PizzaPlaceApi.Domain.DTOs;
using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync(int pageNumber, int pageSize);
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task CreateOrderAsync(CreateOrderDTO createOrderDto);
        Task<OrderDTO> UpdateOrderAsync(UpdateOrderDTO updateOrderDto);
    }
}

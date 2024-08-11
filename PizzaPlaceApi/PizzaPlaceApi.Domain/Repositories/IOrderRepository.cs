using PizzaPlaceApi.Domain.DTOs;
using PizzaPlaceApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaPlaceApi.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderDTO> GetOrderByIdAsync (int orderId);
        Task<List<OrderDTO>> GetAllOrdersAsync(int pageNumber, int pageSize);

        Task CreateOrderAsync(Order order);
        Task CreateOrderDetailsAsync(List<OrderDetails> orderDetails);
    }
}

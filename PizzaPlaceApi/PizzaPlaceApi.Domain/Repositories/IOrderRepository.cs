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
        
        
        Task UpdateOrderAsync(Order order);
        Task RemoveOrderDetailsAsync(OrderDetails orderDetails);
        Task AddOrderDetailsAsync(OrderDetails orderDetails);
        Task UpdateOrderDetailsAsync (OrderDetails orderDetails);


        // Optionally, if you need methods for getting details by order ID
        Task<IEnumerable<OrderDetails>> GetOrderDetailsByOrderIdAsync(int orderId);

        Task DeleteOrderByIdAsync(int orderId);

    }
}

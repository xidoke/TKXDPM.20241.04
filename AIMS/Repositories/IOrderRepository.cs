using AIMS.Models.Entities;

namespace AIMS.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderData> GetOrderByIdAsync(int orderId);
        Task<List<OrderData>> GetAllOrdersAsync();
        Task<int> CreateOrderAsync(OrderData orderData);
        Task UpdateOrderAsync(OrderData orderData);
        Task DeleteOrderAsync(int orderId);
        Task<List<OrderMedia>> GetOrderMediasByOrderIdAsync(int orderId);
        Task AddOrderMediaAsync(OrderMedia orderMedia);
        Task AddOrderMediasAsync(List<OrderMedia> orderMedias);
        Task UpdateOrderMediaAsync(OrderMedia orderMedia);
        Task DeleteOrderMediaAsync(int orderMediaId);
        string GetCurrentUserEmail();
    }
}

using AIMS.Data.Contexts;
using AIMS.Data.Entities;
using AIMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AIMS.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderData> GetOrderByIdAsync(int orderId)
        {
            return await _context.OrderDatas.FindAsync(orderId);
        }

        public async Task<List<OrderData>> GetAllOrdersAsync()
        {
            return await _context.OrderDatas.ToListAsync();
        }

        public async Task<int> CreateOrderAsync(OrderData orderData)
        {
            _context.OrderDatas.Add(orderData);
            await _context.SaveChangesAsync();
            return orderData.Id;
        }

        public async Task UpdateOrderAsync(OrderData orderData)
        {
            _context.OrderDatas.Update(orderData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.OrderDatas.FindAsync(orderId);
            if (order != null)
            {
                _context.OrderDatas.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderMedia>> GetOrderMediasByOrderIdAsync(int orderId)
        {
            return await _context.OrderMedias.Where(om => om.OrderId == orderId).ToListAsync();
        }

        public async Task AddOrderMediaAsync(OrderMedia orderMedia)
        {
            _context.OrderMedias.Add(orderMedia);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrderMediasAsync(List<OrderMedia> orderMedias)
        {
            _context.OrderMedias.AddRange(orderMedias);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateOrderMediaAsync(OrderMedia orderMedia)
        {
            _context.OrderMedias.Update(orderMedia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderMediaAsync(int orderMediaId)
        {
            var orderMedia = await _context.OrderMedias.FindAsync(orderMediaId);
            if (orderMedia != null)
            {
                _context.OrderMedias.Remove(orderMedia);
                await _context.SaveChangesAsync();
            }
        }
    }
}

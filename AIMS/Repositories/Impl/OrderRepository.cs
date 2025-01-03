using AIMS.Data.Contexts;
using AIMS.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AIMS.Repositories.Impl
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetCurrentUserEmail()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
        }
        public async Task<OrderData> GetOrderByIdAsync(int orderId)
        {
            string currentUserEmail = GetCurrentUserEmail();
            return await _context.OrderDatas.Where(o => o.Id == orderId && o.Email == currentUserEmail).FirstOrDefaultAsync();
        }

        public async Task<List<OrderData>> GetAllOrdersAsync()
        {
            string currentUserEmail = GetCurrentUserEmail();
            return await _context.OrderDatas.Where(o => o.Email == currentUserEmail).ToListAsync();
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
            string currentUserEmail = GetCurrentUserEmail();
            var order = await _context.OrderDatas.Where(o => o.Id == orderId && o.Email == currentUserEmail).FirstOrDefaultAsync();
            if (order != null)
            {
                _context.OrderDatas.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderMedia>> GetOrderMediasByOrderIdAsync(int orderId)
        {
            string currentUserEmail = GetCurrentUserEmail();
            var order = await _context.OrderDatas.Where(o => o.Id == orderId && o.Email == currentUserEmail).FirstOrDefaultAsync();
            if (order == null) return new List<OrderMedia>();
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
            var orderMedia = await _context.OrderMedias.Where(om => om.Id == orderMediaId).FirstOrDefaultAsync();
            if (orderMedia != null)
            {
                _context.OrderMedias.Remove(orderMedia);
                await _context.SaveChangesAsync();
            }
        }
    }
}

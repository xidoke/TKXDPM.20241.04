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

        public OrderData CreateOrder(OrderData order)
        {
            _context.OrderDatas.Add(order);
            _context.SaveChanges();
            return order;
        }

        public OrderData GetOrderById(int orderId)
        {
            return _context.OrderDatas.Include(o => o.OrderMedias)
                .ThenInclude(om => om.Media)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public List<OrderData> GetOrdersByUserId(int userId)
        {
            return _context.OrderDatas
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderMedias)
                .ThenInclude(om => om.Media)
                .ToList();
        }
    }
}

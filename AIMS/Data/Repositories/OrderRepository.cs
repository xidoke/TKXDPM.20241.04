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
    }
}

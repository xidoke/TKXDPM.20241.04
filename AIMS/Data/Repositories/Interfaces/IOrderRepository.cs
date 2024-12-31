using AIMS.Data.Entities;

namespace AIMS.Data.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        OrderData CreateOrder(OrderData order);
    }
}

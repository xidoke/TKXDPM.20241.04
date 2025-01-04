using AIMS.Data.Entities;
using AIMS.ViewModels;

namespace AIMS.Service
{
    public interface IEmailService
    {
        Task SendOrderDetailsAsync(OrderDetailsEmailViewModel order);
    }
}

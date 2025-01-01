using AIMS.Data.Entities;
using AIMS.ViewModels;

namespace AIMS.Service.Interfaces
{
    public interface IEmailService
    {
        Task SendOrderDetailsAsync(OrderDetailsEmailViewModel order);
    }
}

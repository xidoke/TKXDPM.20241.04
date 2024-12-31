using AIMS.Models;

namespace AIMS.Service.Interfaces
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, VnPayRequest model);
        VnPayResponse PaymentExecute(IQueryCollection collections);
    }
}

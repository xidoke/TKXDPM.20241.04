using AIMS.Models.Entities;

namespace AIMS.Service
{
    public interface IShippingFeeService
    {
        Task<int> CalculateShippingFee(List<OrderMedia> list, string province, bool isRushOrder);
    }
}

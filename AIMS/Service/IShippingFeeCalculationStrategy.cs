using AIMS.Models.Entities;

namespace AIMS.Service
{
    public interface IShippingFeeCalculationStrategy
    {
        Task<int> CalculateShippingFee(List<OrderMedia> list, string province, bool isRushOrder);
    }
}

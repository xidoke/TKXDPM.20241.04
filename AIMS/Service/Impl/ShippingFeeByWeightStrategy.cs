using AIMS.Models.Entities;
using AIMS.Repositories;

namespace AIMS.Service.Impl
{
    public class ShippingFeeByWeightStrategy : IShippingFeeCalculationStrategy
    {
        private readonly IMediaRepository _mediaRepository;

        public ShippingFeeByWeightStrategy(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<int> CalculateShippingFee(List<OrderMedia> list, string province, bool isRushOrder)
        {
            bool isInnerCity = province.Contains("Thành phố Hà Nội") || province.Contains("Thành phố Hồ Chí Minh");
            int totalItemValue = 0;
            double totalWeight = 0;
            double heaviestItemWeight = 0;
            int rushOrderFee = 0;

            foreach (var item in list)
            {
                var media = await _mediaRepository.GetByIdAsync(item.MediaId);
                double itemWeight = item.Quantity * media.Weight;
                totalWeight += itemWeight;
                heaviestItemWeight = Math.Max(heaviestItemWeight, itemWeight);

                if (media.RushSupport)
                    rushOrderFee += 10000 * item.Quantity;

                totalItemValue += media.Value;
            }

            int baseFee;
            double weightLimit = isInnerCity ? 3 : 0.5;
            int initialPrice = isInnerCity ? 22000 : 30000;
            int additionalFeePerUnit = 2500;

            if (heaviestItemWeight <= weightLimit)
                baseFee = initialPrice;
            else
            {
                double excessWeight = heaviestItemWeight - weightLimit;
                int additionalUnits = (int)Math.Ceiling(excessWeight / 0.5);
                baseFee = initialPrice + (additionalUnits * additionalFeePerUnit);
            }

            int freeShippingDiscount = 0;
            if (totalItemValue > 100000)
                freeShippingDiscount = Math.Min(baseFee, 25000);

            return isRushOrder ? Math.Max(0, Math.Abs(baseFee - freeShippingDiscount)) + rushOrderFee : Math.Max(0, baseFee - freeShippingDiscount);
        }
    }
}

using AIMS.Models.Entities;
using AIMS.Repositories;

namespace AIMS.Service.Impl
{
    public class ShippingFeeService : IShippingFeeService
    {
        private readonly IMediaRepository _mediaRepository;

        public ShippingFeeService(IMediaRepository mediaRepository)
        {
            _mediaRepository = mediaRepository;
        }

        public async Task<int> CalculateShippingFee(List<OrderMedia> orderMediaList, string province, bool isRushOrder)
        {
            IShippingFeeCalculationStrategy selectedStrategy;
            selectedStrategy = new ShippingFeeByWeightStrategy(_mediaRepository);

            return await selectedStrategy.CalculateShippingFee(orderMediaList, province, isRushOrder);
        }
    }
}

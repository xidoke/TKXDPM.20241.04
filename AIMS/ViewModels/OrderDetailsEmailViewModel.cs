using AIMS.Models.Entities;

namespace AIMS.ViewModels
{
    public class OrderDetailsEmailViewModel
    {
        public OrderData Order { get; set; }
        public List<OrderMedia> OrderMedias { get; set; }
        public decimal TotalProductPriceExcludingVAT { get; set; }
        public decimal TotalProductPriceIncludingVAT { get; set; }
        public string TransactionId { get; set; }
        public string TransactionContent { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}

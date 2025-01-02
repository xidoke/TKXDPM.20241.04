using AIMS.Data.Entities;

namespace AIMS.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderData Order { get; set; }
        public List<OrderMedia> OrderMedias { get; set; }
    }
}
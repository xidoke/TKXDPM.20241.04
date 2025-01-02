namespace AIMS.ViewModels
{
    public class UpdateCartRequest
    {
        public int productId { get; set; }
        public int quantity { get; set; }
        public bool isSelected { get; set; }
    }
}

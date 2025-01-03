namespace AIMS.Models.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public bool isSelected { get; set; }
        public int MediaID { get; set; }
        public string MediaName { get; set; }
        public string MediaImgUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}

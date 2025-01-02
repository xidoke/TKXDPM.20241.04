namespace AIMS.ViewModels
{
    public class PaymentResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public string OrderId { get; set; }
    }
}

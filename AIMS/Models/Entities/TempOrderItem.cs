namespace AIMS.Models.Entities
{
    public class TempOrderItem
    {
        public int mediaID { get; set; }
        public string mediaName { get; set; }
        public int price { get; set; }
        public bool isReady = true;
        public int quantity { get; set; }
    }
}

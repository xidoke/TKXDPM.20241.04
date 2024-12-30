namespace AIMS.Data.Entities
{
    public class OrderMedia
    {
        private int _id;
        private int _price;
        private int _quantity;
        private int _orderId;
        private int _mediaId;
        private OrderData _order;
        private Media _media;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public int Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public int OrderId
        {
            get { return _orderId; }
            set { _orderId = value; }
        }

        public int MediaId
        {
            get { return _mediaId; }
            set { _mediaId = value; }
        }

        // Navigation properties
        public OrderData Order
        {
            get { return _order; }
            set { _order = value; }
        }

        public Media Media
        {
            get { return _media; }
            set { _media = value; }
        }
    }
}

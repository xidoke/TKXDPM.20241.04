namespace AIMS.Data.Entities
{
    public class OrderMedia
    {
        private int _id;
        private int _price;
        private int _quantity;
        private int _orderId;
        private int _mediaId;
        private string _mediaName;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Name
        {
            get { return _mediaName; }
            set { _mediaName = value; }
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
    }
}

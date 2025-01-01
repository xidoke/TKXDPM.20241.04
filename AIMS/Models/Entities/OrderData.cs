namespace AIMS.Data.Entities
{
    public class OrderData
    {
        private int _id;
        private string _city;
        private string _address;
        private string _phone;
        private string _email;
        private float _shippingFee;
        private DateTime _createdAt;
        private string _instructions;
        private string _type;
        private float _totalPrice;
        private int _status;
        private string _fullname;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public float ShippingFee
        {
            get { return _shippingFee; }
            set { _shippingFee = value; }
        }
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; }
        }

        public string Instructions
        {
            get { return _instructions; }
            set { _instructions = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        public float TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }

    }
}

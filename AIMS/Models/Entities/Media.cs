namespace AIMS.Models.Entities
{
    public class Media
    {
        private int _id;
        private string _category;
        private string _type;
        private int _price;
        private int _quantity;
        private string _title;
        private string _imgUrl;
        private bool _rushSupport;
        private float _weight;
        private int _value;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public string Type
        {
            get { return _type; }
            set { _type = value; }
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

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string ImgUrl
        {
            get { return _imgUrl; }
            set { _imgUrl = value; }
        }

        public bool RushSupport
        {
            get { return _rushSupport; }
            set { _rushSupport = value; }
        }

        public float Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }
    }
}

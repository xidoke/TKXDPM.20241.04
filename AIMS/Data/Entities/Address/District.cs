namespace AIMS.Data.Entities.Address
{
    public class District
    {
        private string _id;
        private string _name;
        private string _provinceId;
        private Province _province;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string ProvinceId
        {
            get { return _provinceId; }
            set { _provinceId = value; }
        }
        public Province Province
        {
            get { return _province; }
            set { _province = value; }
        }
    }
}

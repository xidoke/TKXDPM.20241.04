namespace AIMS.Data.Entities.Address
{
    public class Ward
    {
        private string _id;
        private string _name;
        private string _districtId;
        private District _district;

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
        public string DistrictId
        {
            get { return _districtId; }
            set { _districtId = value; }
        }
        public District District
        {
            get { return _district; }
            set { _district = value; }
        }
    }
}

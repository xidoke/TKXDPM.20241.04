namespace AIMS.Data.Entities
{
    public class User
    {
        private int _id;
        private string _fullname;
        private string _username;
        private string _email;
        private string _password;
        private string _salt;
        private bool _admin;
        private int _status;
        private string _address;
        private string _phone;
        private string _city;
        private string _deviceID;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Fullname
        {
            get { return _fullname; }
            set { _fullname = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }
        public bool Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }
        public int Status
        {
            get { return _status; }
            set { _status = value; }
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
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public string DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }
    }
}

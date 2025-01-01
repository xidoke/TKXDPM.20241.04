using System.ComponentModel.DataAnnotations;

namespace AIMS.Data.Entities
{
    public class User
    {
        private int _id;
        private string _fullname;
        private string _username;
        private string _email;
        private string _password;
        private string _phone;

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
        
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

    }
}
﻿using System.ComponentModel.DataAnnotations;

namespace AIMS.Models.Entities
{
    public class User
    {
        private int _id;
        private string _fullname;
        private string _username;
        private string _email;
        private string _password;
        private string _phone;
        private string _salt;
        private int _admin;
        private string _status;
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }
        public int Admin
        {
            get { return _admin; }
            set { _admin = value; }
        }
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
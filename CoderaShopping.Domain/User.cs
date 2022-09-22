using System;

namespace CoderaShopping.Domain
{
    public class User
    {
        private Guid _id;
        private string _name;
        private string _email;
        private string _phone;
        private UserType _userType;

        protected User() { }

        public User(string name, string email, string phone, UserType userType) : this()
        {
            _name = name;
            _email = email;
            _phone = phone;
            _userType = userType;
        }

        public virtual Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public virtual string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public virtual UserType UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }
    }
}

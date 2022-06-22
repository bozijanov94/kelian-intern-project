using System;

namespace CoderaShopping.Domain
{
    public class User
    {
        private Guid _id;
        private string _name;
        

        protected User() { }

        public User(string name)
        {
            _name = name;
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
    }
}

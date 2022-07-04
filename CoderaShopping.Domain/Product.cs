using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderaShopping.Domain
{
    public class Product
    {
        private Guid _id;
        private string _name;
        private string _description;
        private Category _category;

        protected Product() { }
        public Product(string name, string description, Category category)
        {
            _name = name;
            _description = description;
            _category = category;
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

        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public virtual Category Category
        {
            get { return _category; }
            set { _category = value; }
        }
    }
}

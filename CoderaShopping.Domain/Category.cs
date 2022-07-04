using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderaShopping.Domain
{
    public class Category
    {
        private Guid _id;
        private string _name;
        private IList<Product> _products;

        protected Category()
        {
            _products = new List<Product>();
        }

        public Category(string name)
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

        public virtual IList<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }
    }
}

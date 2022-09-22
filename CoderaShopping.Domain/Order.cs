using System;

namespace CoderaShopping.Domain
{
    public class Order
    {
        private Guid _id;
        private User _customer;
        private Product _product;
        private int _quantity;
        private DateTime _dateOrdered;

        protected Order() { }

        public Order(User customer, Product product, int quantity, DateTime dateOrdered)
        {
            _customer = customer;
            _product = product;
            _quantity = quantity;
            _dateOrdered = dateOrdered;
        }

        public virtual Guid Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public virtual User Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        public virtual Product Product
        {
            get { return _product; }
            set { _product = value; }
        }

        public virtual int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        public virtual DateTime DateOrdered
        {
            get { return _dateOrdered; }
            set { _dateOrdered = value; }
        }
    }
}

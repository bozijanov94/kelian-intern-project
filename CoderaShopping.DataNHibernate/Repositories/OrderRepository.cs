using CoderaShopping.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        bool IsUnique(Order order);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool IsUnique(Order order)
        {
            return !Session.Query<Order>().Any(x => x.Id != order.Id && (x.Customer.Name == order.Customer.Name));
        }

    }
}

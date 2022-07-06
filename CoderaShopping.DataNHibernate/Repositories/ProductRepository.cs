using CoderaShopping.Domain;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        bool IsUnique(Product product);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool IsUnique(Product product)
        {
            return !Session.Query<Product>().Any(x => x.Id != product.Id && x.Name == product.Name);
        }
    }
}

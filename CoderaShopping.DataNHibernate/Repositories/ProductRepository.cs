using CoderaShopping.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        bool IsUnique(Product product);
        IList<Product> GetAll(string search);
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool IsUnique(Product product)
        {
            return !Session.Query<Product>().Any(x => x.Id != product.Id && x.Name == product.Name);
        }

        public IList<Product> GetAll(string search)
        {
            var products = Session.Query<Product>().Where(x => x.Name.ToLower().Contains(search.ToLower()));
            return products.ToList();
        }
    }
}

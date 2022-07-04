using CoderaShopping.Domain;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool IsUnique(Category category);
    }

    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool IsUnique(Category category)
        {
            return !Session.Query<Category>().Any(x => x.Id != category.Id && x.Name == category.Name);
        }
    }
}

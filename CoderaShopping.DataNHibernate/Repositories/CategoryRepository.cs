using CoderaShopping.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool IsUnique(Category category);
        IList<Category> GetAll(string search, bool? status = null);
    }

    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool IsUnique(Category category)
        {
            return !Session.Query<Category>().Any(x => x.Id != category.Id && x.Name == category.Name);
        }

        public IList<Category> GetAll(string search, bool? status = null)
        {
            var categories = Session.Query<Category>().Where(x => x.Name.ToLower().Contains(search.ToLower()));
            if (status.HasValue)
            {
                categories = categories.Where(x => x.Status == status.Value);
            }
            return categories.ToList();
        }
    }
}

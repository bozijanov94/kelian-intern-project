using CoderaShopping.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUnique(User user);
        IList<User> GetAll(string search);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool IsUnique(User user)
        {
            return !Session.Query<User>().Any(x => x.Id != user.Id && (x.Email == user.Email || x.Phone == user.Phone));
        }

        public IList<User> GetAll(string search)
        {
            var users = Session.Query<User>().Where(x => x.Name.ToLower().Contains(search.ToLower()));
            return users.ToList();
        }

    }
}

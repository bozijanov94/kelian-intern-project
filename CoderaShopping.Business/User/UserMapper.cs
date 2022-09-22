using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel MapToViewModel(this User user)
        {
            var viewModel = new UserViewModel();
            viewModel.Id = user.Id;
            viewModel.Name = user.Name;
            viewModel.Email = user.Email;
            viewModel.Phone = user.Phone;
            viewModel.UserType = user.UserType;

            return viewModel;
        }

        public static LookupViewModel MapToLookupViewModel(this User user)
        {
            var viewModel = new LookupViewModel();
            viewModel.Id = user.Id;
            viewModel.Name = user.Name;

            return viewModel;
        }
    }
}

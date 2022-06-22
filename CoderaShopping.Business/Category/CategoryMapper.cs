using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryViewModel MapToViewModel(this Category category)
        {
            var viewModel = new CategoryViewModel();
            viewModel.Id = category.Id;
            viewModel.Name = category.Name;

            return viewModel;
        }
    }
}

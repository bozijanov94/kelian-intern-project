using CoderaShopping.Business.Models;
using CoderaShopping.Domain;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.Business.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryViewModel MapToViewModel(this Category category)
        {
            var viewModel = new CategoryViewModel();
            viewModel.Id = category.Id;
            viewModel.Name = category.Name;
            viewModel.Products = category.Products.Select(x => x.MapToLookupViewModel()).ToList();

            return viewModel;
        }

        public static LookupViewModel MapToLookupViewModel(this Category category)
        {
            var viewModel = new LookupViewModel();
            viewModel.Id = category.Id;
            viewModel.Name = category.Name;

            return viewModel;
        }
    }
}

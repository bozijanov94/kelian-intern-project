﻿using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class ProductMapper
    {
        public static ProductViewModel MapToViewModel(this Product product)
        {
            var viewModel = new ProductViewModel();
            viewModel.Id = product.Id;
            viewModel.Name = product.Name;
            viewModel.Description = product.Description;
            viewModel.Category = product.Category.MapToLookupViewModel();

            return viewModel;
        }

        public static LookupViewModel MapToLookupViewModel(this Product product)
        {
            var viewModel = new LookupViewModel();
            viewModel.Id = product.Id;
            viewModel.Name = product.Name;

            return viewModel;
        }
    }
}

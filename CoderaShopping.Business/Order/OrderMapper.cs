using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class OrderMapper
    {
        public static OrderViewModel MapToViewModel(this Order order)
        {
            var viewModel = new OrderViewModel();
            viewModel.Id = order.Id;
            viewModel.Customer = UserMapper.MapToLookupViewModel(order.Customer);
            viewModel.Product = ProductMapper.MapToLookupViewModel(order.Product);
            viewModel.Quantity = order.Quantity;
            viewModel.DateOrdered = order.DateOrdered.ToString();

            return viewModel;
        }

        public static LookupViewModel MapToLookupViewModel(this Order order)
        {
            var viewModel = new LookupViewModel();
            viewModel.Id = order.Id;

            return viewModel;
        }
    }
}

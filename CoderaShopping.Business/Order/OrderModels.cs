using CoderaShopping.Domain;
using FluentValidation;
using FluentValidation.Attributes;
using System;

namespace CoderaShopping.Business.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public LookupViewModel Customer { get; set; }
        public LookupViewModel Product { get; set; }
        public int Quantity { get; set; }
        public string DateOrdered { get; set; }
    }

    [Validator(typeof(OrderCreateViewModelValidator))]
    public class OrderCreateViewModel
    {
        public LookupViewModel Customer { get; set; }
        public LookupViewModel Product { get; set; }
        public int Quantity { get; set; }
    }

    public class OrderCreateViewModelValidator : AbstractValidator<OrderCreateViewModel>
    {
        public OrderCreateViewModelValidator()
        {
            RuleFor(c => c.Customer).NotEmpty().WithMessage(CustomMessages.Order.NAME_EMPTY);
        }
    }

    public class OrderFilterModel
    {
        public User Customer { get; set; }
        public Product Product { set; get; }
    }
}

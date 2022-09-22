using FluentValidation;
using FluentValidation.Attributes;
using System;

namespace CoderaShopping.Business.Models
{

    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public LookupViewModel Category { get; set; }
    }

    [Validator(typeof(ProductCreateViewModelValidator))]
    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public LookupViewModel Category { get; set; }
    }

    public class ProductCreateViewModelValidator : AbstractValidator<ProductCreateViewModel>
    {
        public ProductCreateViewModelValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(CustomMessages.Product.NAME_EMPTY);
            RuleFor(c => c.Category).NotEmpty().WithMessage(CustomMessages.Product.CATEGORY_EMPTY);
        }
    }

    public class ProductFilterModel
    {
        public string Name { get; set; }
        public LookupViewModel Category { get; set; }

    }
}

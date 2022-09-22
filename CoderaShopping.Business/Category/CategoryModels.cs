using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;

namespace CoderaShopping.Business.Models
{

    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool IsDefault { get; set; }
        public IList<LookupViewModel> Products { get; set; }
        public CategoryViewModel()
        {
            Products = new List<LookupViewModel>();
        }

    }

    [Validator(typeof(CategoryCreateViewModelValidator))]
    public class CategoryCreateViewModel
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool IsDefault { get; set; }
    }

    public class CategoryCreateViewModelValidator : AbstractValidator<CategoryCreateViewModel>
    {
        public CategoryCreateViewModelValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(CustomMessages.Category.NAME_EMPTY);
        }
    }

    public class CategoryFilterModel
    {
        public string Name { get; set; }
        public IdentityLookupViewModel Status { get; set; }
        public IdentityLookupViewModel IsDefault { get; set; }

    }
}

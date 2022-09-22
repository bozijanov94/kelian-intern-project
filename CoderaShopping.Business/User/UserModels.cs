using CoderaShopping.Domain;
using FluentValidation;
using FluentValidation.Attributes;
using System;

namespace CoderaShopping.Business.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
    }

    [Validator(typeof(UserCreateViewModelValidator))]
    public class UserCreateViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserType UserType { get; set; }
    }

    public class UserCreateViewModelValidator : AbstractValidator<UserCreateViewModel>
    {
        public UserCreateViewModelValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage(CustomMessages.User.NAME_EMPTY);
        }
    }

    public class UserFilterModel
    {
        public string Name { get; set; }
        public string Email { set; get; }
        public IdentityLookupViewModel UserType { get; set; }
    }
}

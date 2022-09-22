using FluentValidation.Mvc;

namespace CoderaShopping.Business
{
    public static class CustomMessages
    {
        public static class Category
        {
            public const string NOT_FOUND = "Category not found!";
            public const string NAME_EMPTY = "Category name is required!";
            public const string SUCCESS_CREATE = "Category successfully created!";
            public const string SUCCESS_DELETE = "Category successfully deleted!";
            public const string SUCCESS_EDIT = "Category successfully edited!";
            public const string ERROR_DELETE_DEFAULT = "Cannot delete default category. Replace first!";
            public const string ERROR_CREATE_EXISTS = "Category already exists!";
            public const string ERROR_CREATE_INACTIVE_DEFAULT = "Cannot have inactive default category!";
            public const string ERROR_EDIT_INACTIVE_DEFAULT = "Cannot make default category false without replacing first!";
            public const string ERROR_EDIT_DEFAULT_INACTIVE = "Cannot make category default if status is inactive!";
        }

        public static class Product
        {
            public const string NOT_FOUND = "Product not found!";
            public const string NAME_EMPTY = "Product name is required!";
            public const string CATEGORY_EMPTY = "Product needs a Category!";
            public const string SUCCESS_CREATE = "Product successfully created!";
            public const string SUCCESS_DELETE = "Product successfully deleted!";
            public const string SUCCESS_EDIT = "Product successfully edited!";

            public static string ERROR_COL_NAME(string name)
            {
                return $"Cannot order by column: {name}!";
            }
        }

        public static class User
        {
            public const string NAME_EMPTY = "User name is required";
            public const string ERROR_CREATE_EXISTS = "User already exists!";
            public const string SUCCESS_CREATE = "User successfully created!";
            public const string SUCCESS_DELETE = "User successfully deleted!";
            public const string SUCCESS_EDIT = "User successfully edited!";
        }

        public static class Order
        {
            public const string NAME_EMPTY = "Order customer name is required";
            public const string ERROR_CREATE_EXISTS = "Order already exists!";
            public const string SUCCESS_CREATE = "Order successfully created!";
            public const string SUCCESS_DELETE = "Order successfully deleted!";
            public const string SUCCESS_EDIT = "Order successfully edited!";
        }
    }
}

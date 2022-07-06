using System;
using System.Collections.Generic;

namespace CoderaShopping.Business.Models
{

    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<LookupViewModel> Products { get; set; }
        public CategoryViewModel()
        {
            Products = new List<LookupViewModel>();
        }

    }

    public class CategoryCreateViewModel
    {
        public string Name { get; set; }
    }
}

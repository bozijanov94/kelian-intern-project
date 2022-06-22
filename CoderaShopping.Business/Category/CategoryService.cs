using System.Collections.Generic;
using System.Linq;
using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;

namespace CoderaShopping.Business.Services
{
    public interface ICategoryService
    {
        List<CategoryViewModel> GetAll();
    }

    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }



        public List<CategoryViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var categories = _categoryRepository.GetAll().Select(x => x.MapToViewModel()).ToList();
            
            _unitOfWork.Commit();

            return categories;
        }
    }
}

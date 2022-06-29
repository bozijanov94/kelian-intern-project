using System;
using System.Collections.Generic;
using System.Linq;
using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Services
{
    public interface ICategoryService
    {
        List<CategoryViewModel> GetAll();
        CategoryViewModel GetById(Guid id);
        CategoryViewModel Create(CategoryCreateViewModel model);
        CategoryViewModel Delete(Guid id);
        CategoryViewModel Update(CategoryViewModel model);
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

        public CategoryViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var category = _categoryRepository.GetById(id).MapToViewModel();

            _unitOfWork.Commit();

            return category;
        }

        public CategoryViewModel Create(CategoryCreateViewModel model)
        {
            var domainCategory = new Category(model.Name);

            _unitOfWork.BeginTransaction();

            if (_categoryRepository.IsUnique(domainCategory))
            {
                _categoryRepository.Add(domainCategory);
            }

            _unitOfWork.Commit();

            return domainCategory.MapToViewModel();
        }

        public CategoryViewModel Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var category = _categoryRepository.GetById(id);

            _categoryRepository.Delete(category);

            _unitOfWork.Commit();

            return category.MapToViewModel();
        }

        public CategoryViewModel Update(CategoryViewModel model)
        {
            var updateCategory = _categoryRepository.GetById(model.Id);

            if (model.Name != null)
            {
                updateCategory.Name = model.Name;
            }

            _unitOfWork.BeginTransaction();

            _categoryRepository.Update(updateCategory);

            _unitOfWork.Commit();

            return updateCategory.MapToViewModel();
        }
    }
}

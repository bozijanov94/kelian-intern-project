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
        GridResult<CategoryViewModel> GetAll(int currentPage, int itemsPerPage, CategoryFilterModel filter, bool orderAscend, string orderBy);
        List<CategoryViewModel> GetAllAvailable(string search, bool? status = null);
        CategoryViewModel GetById(Guid id);
        void Create(CategoryCreateViewModel model);
        void Delete(Guid id);
        void Update(CategoryViewModel model);
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

        public GridResult<CategoryViewModel> GetAll(int currentPage, int itemsPerPage, CategoryFilterModel filter, bool orderAscend, string orderBy)
        {
            _unitOfWork.BeginTransaction();

            var categories = _categoryRepository.GetAll();

            #region filters
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                categories = categories.Where(c => c.Name.ToLower().Contains(filter.Name.ToLower()));
            }
            if (filter.Status != null)
            {
                categories = categories.Where(c => (c.Status && filter.Status.Id == 1) || (!c.Status && filter.Status.Id == 0));
            }
            if (filter.IsDefault != null)
            {
                categories = categories.Where(c => (c.IsDefault && filter.IsDefault.Id == 1) || (!c.IsDefault && filter.IsDefault.Id == 0));
            }
            #endregion

            #region sort
            switch (orderBy)
            {
                case "Name":
                    categories = orderAscend ? categories.OrderBy(c => c.Name) : categories.OrderByDescending(c => c.Name);
                    break;
                case "Status":
                    categories = orderAscend ? categories.OrderBy(c => c.Status) : categories.OrderByDescending(c => c.Status);
                    break;
                case "IsDefault":
                    categories = orderAscend ? categories.OrderBy(c => c.IsDefault) : categories.OrderByDescending(c => c.IsDefault);
                    break;
                default:
                    throw new Exception($"Cannot order by column: {orderBy}!");
            }
            #endregion

            var totalItems = categories.Count();

            categories = categories.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);

            var categoriesToDisplay = new GridResult<CategoryViewModel>
            {
                Items = categories.Select(x => x.MapToViewModel()).ToList(),
                TotalItems = totalItems
            };

            _unitOfWork.Commit();

            return categoriesToDisplay;
        }

        public List<CategoryViewModel> GetAllAvailable(string search, bool? status = null)
        {
            _unitOfWork.BeginTransaction();

            var categories = _categoryRepository.GetAll(search, status).OrderByDescending(x => x.IsDefault).Select(x => x.MapToViewModel()).ToList();

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

        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var category = _categoryRepository.GetById(id);

            if (category.IsDefault == false)
            {
                _categoryRepository.Delete(category);
            }
            else
            {
                throw new Exception(CustomMessages.Category.ERROR_DELETE_DEFAULT);
            }

            _unitOfWork.Commit();
        }

        public void Create(CategoryCreateViewModel model)
        {
            var category = new Category(model.Name, model.Status, model.IsDefault);

            _unitOfWork.BeginTransaction();

            if (_categoryRepository.IsUnique(category))
            {
                CreateUpdateDefault(model);

                _categoryRepository.Add(category);

                _unitOfWork.Commit();
            }
            else
            {
                _unitOfWork.Rollback();

                throw new Exception(CustomMessages.Category.ERROR_CREATE_EXISTS);
            }
        }

        public void Update(CategoryViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var updateCategory = _categoryRepository.GetById(model.Id);

            if (model.Name != null)
            {
                updateCategory.Name = model.Name;
            }

            UpdateUpdateDefault(model);

            updateCategory.Status = model.Status;

            updateCategory.IsDefault = model.IsDefault;

            _categoryRepository.Update(updateCategory);

            _unitOfWork.Commit();
        }

        private void CreateUpdateDefault(CategoryCreateViewModel model)
        {
            var defaultCategory = _categoryRepository.GetAll().FirstOrDefault(x => x.IsDefault);

            if (model.Status == false && model.IsDefault == true)
            {
                throw new Exception(CustomMessages.Category.ERROR_CREATE_INACTIVE_DEFAULT);
            }
            else if (model.IsDefault == true && defaultCategory != null)
            {
                defaultCategory.IsDefault = false;

                _categoryRepository.Update(defaultCategory);
            }
        }

        private void UpdateUpdateDefault(CategoryViewModel category)
        {
            var defaultCategory = _categoryRepository.GetAll().FirstOrDefault(x => x.IsDefault);

            if (defaultCategory.Id == category.Id && category.IsDefault == false)
            {
                throw new Exception(CustomMessages.Category.ERROR_EDIT_INACTIVE_DEFAULT);
            }
            else if (category.Status == false && category.IsDefault == true)
            {
                throw new Exception(CustomMessages.Category.ERROR_EDIT_DEFAULT_INACTIVE);
            }
            else if (category.IsDefault == true && category.Id != defaultCategory.Id)
            {
                defaultCategory.IsDefault = false;

                _categoryRepository.Update(defaultCategory);
            }
        }
    }
}

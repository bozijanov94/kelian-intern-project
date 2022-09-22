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
    public interface IProductService
    {
        GridResult<ProductViewModel> GetAll(int currentPage, int itemsPerPage, ProductFilterModel filter, bool orderAscend, string orderBy);
        List<ProductViewModel> GetAllSearch(string search);
        ProductViewModel GetById(Guid id);
        void Create(ProductCreateViewModel model);
        void Delete(Guid id);
        void Update(ProductViewModel model);
    }

    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, 
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public GridResult<ProductViewModel> GetAll(int currentPage, int itemsPerPage, ProductFilterModel filter, bool orderAscend, string orderBy)
        {
            _unitOfWork.BeginTransaction();

            var products = _productRepository.GetAll();


            #region filters
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                products = products.Where(c => c.Name.ToLower().Contains(filter.Name.ToLower()));
            }
            if (filter.Category != null)
            {
                products = products.Where(c => c.Category.Name.Equals(filter.Category.Name));
            }
            #endregion

            #region sort
            switch (orderBy)
            {
                case "Name":
                    products = orderAscend ? products.OrderBy(c => c.Name) : products.OrderByDescending(c => c.Name);
                    break;
                case "Category":
                    products = orderAscend ? products.OrderBy(c => c.Category.Name) : products.OrderByDescending(c => c.Category.Name);
                    break;
                default:
                    throw new Exception(CustomMessages.Product.ERROR_COL_NAME(orderBy));
            }
            #endregion


            var totalItems = products.Count();

            products = products.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);

            var productsToDisplay = new GridResult<ProductViewModel>
            {
                Items = products.Select(x => x.MapToViewModel()).ToList(),
                TotalItems = totalItems
            };

            _unitOfWork.Commit();

            return productsToDisplay;
        }

        public List<ProductViewModel> GetAllSearch(string search)
        {
            _unitOfWork.BeginTransaction();

            var products = _productRepository.GetAll(search).OrderByDescending(x => x.Name).Select(x => x.MapToViewModel()).ToList();

            _unitOfWork.Commit();

            return products;
        }

        public ProductViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var product = _productRepository.GetById(id).MapToViewModel();

            _unitOfWork.Commit();

            return product;
        }

        public void Create(ProductCreateViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var category = _categoryRepository.GetById(model.Category.Id);

            var domainProduct = new Product(model.Name, model.Description, category);

            if (_productRepository.IsUnique(domainProduct))
            {
                _productRepository.Add(domainProduct);
            }

            _unitOfWork.Commit();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var product = _productRepository.GetById(id);

            _productRepository.Delete(product);

            _unitOfWork.Commit();
        }

        public void Update(ProductViewModel model)
        {
            var updateProduct = _productRepository.GetById(model.Id);

            if (model.Name != null)
            {
                updateProduct.Name = model.Name;
            }
            if (model.Description != null)
            {
                updateProduct.Description = model.Description;
            }

            _unitOfWork.BeginTransaction();

            _productRepository.Update(updateProduct);

            _unitOfWork.Commit();
        }
    }
}

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
        List<ProductViewModel> GetAll();
        ProductViewModel GetById(Guid id);
        ProductViewModel Create(ProductCreateViewModel model);
        ProductViewModel Delete(Guid id);
        ProductViewModel Update(ProductViewModel model);
    }

    public class ProductService : IProductService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork, 
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public List<ProductViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var products = _productRepository.GetAll().Select(x => x.MapToViewModel()).ToList();
            
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

        public ProductViewModel Create(ProductCreateViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var category = _categoryRepository.GetById(model.Category.Id);

            var domainProduct = new Product(model.Name, model.Description, category);

            if (_productRepository.IsUnique(domainProduct))
            {
                _productRepository.Add(domainProduct);
            }

            _unitOfWork.Commit();

            return domainProduct.MapToViewModel();
        }

        public ProductViewModel Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var Product = _productRepository.GetById(id);

            _productRepository.Delete(Product);

            _unitOfWork.Commit();

            return Product.MapToViewModel();
        }

        public ProductViewModel Update(ProductViewModel model)
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

            return updateProduct.MapToViewModel();
        }
    }
}

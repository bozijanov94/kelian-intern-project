using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;
using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.Business.Services
{
    public interface IOrderService
    {
        GridResult<OrderViewModel> GetAll(int currentPage, int itemsPerPage, OrderFilterModel filter, bool orderAscend, string orderBy);
        OrderViewModel GetById(Guid id);
        void Create(OrderCreateViewModel model);
        void Delete(Guid id);
        void Update(OrderViewModel model);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(OrderCreateViewModel model)
        {
            var user = _userRepository.GetById(model.Customer.Id);

            var product = _productRepository.GetById(model.Product.Id);

            var order = new Order(user, product, model.Quantity, DateTime.Now);

            _unitOfWork.BeginTransaction();

            if (_orderRepository.IsUnique(order))
            {
                _orderRepository.Add(order);

                _unitOfWork.Commit();
            }
            else
            {
                _unitOfWork.Rollback();

                throw new Exception(CustomMessages.Order.ERROR_CREATE_EXISTS);
            }
        }

        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var order = _orderRepository.GetById(id);

            _orderRepository.Delete(order);

            _unitOfWork.Commit();
        }

        public GridResult<OrderViewModel> GetAll(int currentPage, int itemsPerPage, OrderFilterModel filter, bool orderAscend, string orderBy)
        {
            _unitOfWork.BeginTransaction();

            var orders = _orderRepository.GetAll();

            #region filters
            if (filter.Customer != null)
            {
                orders = orders.Where(c => c.Customer.Name == filter.Customer.Name);
            }
            if (filter.Product != null)
            {
                orders = orders.Where(c => c.Product.Name == filter.Product.Name);
            }
            #endregion

            #region sort
            switch (orderBy)
            {
                case "Customer":
                    orders = orderAscend ? orders.OrderBy(c => c.Customer.Name) : orders.OrderByDescending(c => c.Customer.Name);
                    break;
                case "Product":
                    orders = orderAscend ? orders.OrderBy(c => c.Product.Name) : orders.OrderByDescending(c => c.Product.Name);
                    break;
                case "Quantity":
                    orders = orderAscend ? orders.OrderBy(c => c.Quantity) : orders.OrderByDescending(c => c.Quantity);
                    break;
                case "DateOrdered":
                    orders = orderAscend ? orders.OrderBy(c => c.DateOrdered) : orders.OrderByDescending(c => c.DateOrdered);
                    break;
                default:
                    throw new Exception($"Cannot order by column: {orderBy}!");
            }
            #endregion

            var totalItems = orders.Count();

            orders = orders.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);

            var ordersToDisplay = new GridResult<OrderViewModel>
            {
                Items = orders.Select(x => x.MapToViewModel()).ToList(),
                TotalItems = totalItems
            };

            _unitOfWork.Commit();

            return ordersToDisplay;
        }

        public OrderViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var order = _orderRepository.GetById(id).MapToViewModel();

            _unitOfWork.Commit();

            return order;
        }

        public void Update(OrderViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var updateOrder = _orderRepository.GetById(model.Id);

            if (model.Customer != null)
            {
                updateOrder.Customer = _userRepository.GetById(model.Customer.Id);
            }
            if (model.Product != null)
            {
                updateOrder.Product = _productRepository.GetById(model.Product.Id);
            }

            updateOrder.Quantity = model.Quantity;

            updateOrder.DateOrdered = DateTime.Now;

            _orderRepository.Update(updateOrder);

            _unitOfWork.Commit();
        }
    }
}

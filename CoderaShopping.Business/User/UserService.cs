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
    public interface IUserService
    {
        GridResult<UserViewModel> GetAll(int currentPage, int itemsPerPage, UserFilterModel filter, bool orderAscend, string orderBy);
        List<UserViewModel> GetAllSearch(string search);
        UserViewModel GetById(Guid id);
        void Create(UserCreateViewModel model);
        void Delete(Guid id);
        void Update(UserViewModel model);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public void Create(UserCreateViewModel model)
        {
            var user = new User(model.Name, model.Email, model.Phone, model.UserType);

            _unitOfWork.BeginTransaction();

            if (_userRepository.IsUnique(user))
            {
                _userRepository.Add(user);

                _unitOfWork.Commit();
            }
            else
            {
                _unitOfWork.Rollback();

                throw new Exception(CustomMessages.User.ERROR_CREATE_EXISTS);
            }
        }

        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(id);

            _userRepository.Delete(user);

            _unitOfWork.Commit();
        }

        public GridResult<UserViewModel> GetAll(int currentPage, int itemsPerPage, UserFilterModel filter, bool orderAscend, string orderBy)
        {
            _unitOfWork.BeginTransaction();

            var users = _userRepository.GetAll();

            #region filters
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                users = users.Where(c => c.Name.ToLower().Contains(filter.Name.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                users = users.Where(c => c.Email.ToLower().Contains(filter.Email.ToLower()));
            }
            if (filter.UserType != null)
            {
                users = users.Where(c => c.UserType == (UserType)filter.UserType.Id);
            }
            #endregion

            #region sort
            switch (orderBy)
            {
                case "Name":
                    users = orderAscend ? users.OrderBy(c => c.Name) : users.OrderByDescending(c => c.Name);
                    break;
                case "Email":
                    users = orderAscend ? users.OrderBy(c => c.Email) : users.OrderByDescending(c => c.Email);
                    break;
                case "UserType":
                    users = orderAscend ? users.OrderBy(c => c.UserType) : users.OrderByDescending(c => c.UserType);
                    break;
                default:
                    throw new Exception($"Cannot order by column: {orderBy}!");
            }
            #endregion

            var totalItems = users.Count();

            users = users.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);

            var usersToDisplay = new GridResult<UserViewModel>
            {
                Items = users.Select(x => x.MapToViewModel()).ToList(),
                TotalItems = totalItems
            };

            _unitOfWork.Commit();

            return usersToDisplay;
        }

        public List<UserViewModel> GetAllSearch(string search)
        {
            _unitOfWork.BeginTransaction();

            var users = _userRepository.GetAll(search).OrderByDescending(x => x.Name).Select(x => x.MapToViewModel()).ToList();

            _unitOfWork.Commit();

            return users;
        }

        public UserViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(id).MapToViewModel();

            _unitOfWork.Commit();

            return user;
        }

        public void Update(UserViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var updateUser = _userRepository.GetById(model.Id);

            if (model.Name != null)
            {
                updateUser.Name = model.Name;
            }
            if (model.Email != null)
            {
                updateUser.Email = model.Email;
            }

            updateUser.UserType = model.UserType;

            _userRepository.Update(updateUser);

            _unitOfWork.Commit();
        }
    }
}

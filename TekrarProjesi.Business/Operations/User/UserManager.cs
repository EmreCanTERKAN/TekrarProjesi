using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Business.Operations.User.Dtos;
using TekrarProjesi.Business.Types;
using TekrarProjesi.Data.Entities;
using TekrarProjesi.Data.Repositories;
using TekrarProjesi.Data.UnitOfWork;

namespace TekrarProjesi.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IRepository<UserEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IRepository<UserEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {

            var hasMail = _repository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());

            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Email adresi zaten mevcut"
                };
            }

            var userEntity = new UserEntity
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                BirthDate = user.BirthDate
            };
            _repository.Add(userEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kullanıcı kaydı sırasında bir hata oluştu");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };
        }
    }
}

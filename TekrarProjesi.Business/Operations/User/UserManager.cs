using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Business.DataProtection;
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
        private readonly IDataProtection _dataProtector;

        public UserManager(IRepository<UserEntity> repository, IUnitOfWork unitOfWork , IDataProtection dataprotector)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _dataProtector = dataprotector;
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
                //  Password = user.Password,
                Password = _dataProtector.Protect(user.Password),
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

        public async Task<ServiceMessage<UserInfo>> LoginUser(LoginUserDto user)
        {
            var userEntity = _repository.Get(x => x.Email.ToLower() == user.Email.ToLower());

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfo>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı."
                };
            }

            var unProtectedText = _dataProtector.UnProtect(userEntity.Password);

            if (unProtectedText == user.Password)
            {
                return new ServiceMessage<UserInfo>
                {
                    IsSucceed = true,
                    Data = new UserInfo
                    {
                        Email = userEntity.Email,
                        FirstName = userEntity.FirstName,
                        LastName = userEntity.LastName,
                        UserType = userEntity.UserType,
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfo>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }


        }
    }
}

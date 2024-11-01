using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekrarProjesi.Business.Operations.User.Dtos;
using TekrarProjesi.Business.Types;

namespace TekrarProjesi.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);

        Task<ServiceMessage<UserInfo>> LoginUser(LoginUserDto user);
    }
}

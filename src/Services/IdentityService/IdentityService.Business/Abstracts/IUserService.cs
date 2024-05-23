using IdentityService.Business.Dtos.BaseResponses;
using IdentityService.Business.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Business.Abstracts
{
    public interface IUserService
    {
        Task<Response> Registration(UserRegistrationDto request);
        Task<Response> Login(UserLoginRequestDto request);
        Task<Response> Refresh(string token);
        Task<Response> Logout(string token);
    }
}

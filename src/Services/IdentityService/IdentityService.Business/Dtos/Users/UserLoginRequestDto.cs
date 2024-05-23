using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Business.Dtos.Users
{
    public record UserLoginRequestDto(string EmailAddress, string Password);
    public record TokenResponse(string Jwt, string Refresh);
    public record UserRegistrationDto(string FirstName, string LastName, string EmailAddress, string Password);
}

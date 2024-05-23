using IdentityService.Business.Abstracts;
using IdentityService.Business.Dtos.BaseResponses;
using IdentityService.Business.Dtos.Users;
using IdentityService.Business.Utils;
using IdentityService.Data.Abstracts;
using IdentityService.Entities.Concretes;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.Business.Concretes
{
    public class UserManager(IUserRepository repository) : IUserService
    {
        public async Task<Response> Registration(UserRegistrationDto request)
        {
            var registeredUser = await repository.GetWhere(u=>u.EmailAddress == request.EmailAddress).FirstOrDefaultAsync();

            if (registeredUser != null)
                return new ErrorResponse("Email address is taken.");

            var user = new User()
            {
                Role = Role.Customer,
                EmailAddress = request.EmailAddress,
                TokenExpires = DateTime.Now.AddMinutes(120),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = HashHelper.GetMD5HashBytes(request.Password),
                RefreshToken = GenerateRefreshToken()
            };

            if (repository.GetWhere().Count() < 1)
                user.Role = Role.Admin;
               
            repository.AddAsync(user);

            return new DataResponse<TokenResponse>(new TokenResponse(GenerateJWTToken(user),user.RefreshToken));
        }

        public async Task<Response> Login(UserLoginRequestDto request)
        {
            var user = await repository.GetWhere(u => u.EmailAddress == request.EmailAddress).FirstOrDefaultAsync();
            if (user is null)
                return new ErrorResponse("Email or password wrong.");

            if(!HashHelper.VerifyMD5Hash(request.Password,user.PasswordHash))
                return new ErrorResponse("Email or password wrong.");

            user.RefreshToken = GenerateRefreshToken();
            user.TokenExpires = DateTime.Now.AddMinutes(120);
            await  repository.SaveChangesAsync();

            return new DataResponse<TokenResponse>(new TokenResponse(GenerateJWTToken(user), user.RefreshToken));
        }

        public async Task<Response> Refresh(string token)
        {
            var user = await repository.GetWhere(u => u.RefreshToken == token).FirstOrDefaultAsync();

            if (user is null)
                return new ErrorResponse("User not found.");

            user.RefreshToken = GenerateRefreshToken();
            user.TokenExpires = DateTime.Now.AddMinutes(120);
            await repository.SaveChangesAsync();
            return new DataResponse<TokenResponse>(new TokenResponse(GenerateJWTToken(user), user.RefreshToken));
        }

        public async Task<Response> Logout(string token)
        {
            var user = await repository.GetWhere(u => u.RefreshToken == token).FirstOrDefaultAsync();

            if (user is null)
                return new ErrorResponse("User not found.");

            user.RefreshToken = String.Empty;
            await repository.SaveChangesAsync();
            return new SuccessResponse();
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private string GenerateJWTToken(User user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("dummy_secret_for_development_mode")
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

      
    }
}

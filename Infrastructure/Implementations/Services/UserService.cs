
using Application.Extensions;
using Application.Interfaces.Services;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Persistence;
using System.Security.Claims;

namespace Infrastructure.Implementations.Services {
    public class UserService : GenericService<User>, IUserService {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor) : base(applicationDbContext, httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> LoginUser(LoginModel loginModel) {
            string username = loginModel.Username;
            string password = loginModel.Password;
            bool isPersistent = loginModel.RememberMe;
            User user = await FindAsync(u => u.UserName.Equals(username));
            if (user == null) {
                return false;
            }
            if ($"{password}{user.Salt}".GetHash().Equals(user.Password)) {
                List<Claim> claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, $"{user.firstName} {user.lastName}"),
                    new Claim(ClaimTypes.Role, $"{user.Role}")
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties authenticationProperties = new AuthenticationProperties {
                    IsPersistent = isPersistent,
                    IssuedUtc = DateTime.UtcNow,
                    RedirectUri = "/AdminPanel/Index",
                };
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authenticationProperties);
                return true;
            }
            return false;
        }

        public async Task<bool> LogoutUser () {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return true;
        }
    }
}


using Application.Common;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services {
    public interface IUserService : IGenericService<User> {
        public Task<bool> LoginUser(LoginModel loginModel);
    }
}

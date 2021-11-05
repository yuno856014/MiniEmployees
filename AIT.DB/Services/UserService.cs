using AIT.DB.Models;
using AIT.DB.Models.Accounts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(UserManager<User> userManager,
                            SignInManager<User> signInManager,
                            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        public async Task<LoginResult> Login(Login LoginUser)
        {
            var user = await userManager.FindByNameAsync(LoginUser.Email);
            if (user == null)
            {
                return new LoginResult()
                {
                    UserId = string.Empty,
                    Email = string.Empty,
                    Message = "Người dùng không tồn tại!"
                };
            }
            var signInResult = await signInManager.PasswordSignInAsync(user, LoginUser.Password, LoginUser.RememberMe, false);
            if (signInResult.Succeeded)
            {
                var roles = await userManager.GetRolesAsync(user);
                return new LoginResult()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Message = "Đăng nhập thành công",
                    Roles = roles.ToArray()
                };
            }
            return new LoginResult()
            {
                UserId = string.Empty,
                Email = string.Empty,
                Message = "Đã xảy ra lỗi vui lòng thử lại sau!"
            };
        }

        public void Sighout()
        {
            signInManager.SignOutAsync().Wait();
        }
    }
}

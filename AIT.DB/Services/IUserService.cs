using AIT.DB.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Services
{
    public interface IUserService
    {
        Task<LoginResult> Login(Login LoginUser);
        void Sighout();
    }
}

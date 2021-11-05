using AIT.DB.Models.Accounts;
using AIT.DB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.Login(login);
                if (result.Success && result.Roles.Length > 0)
                {
                    if (result.Roles.Contains("SystemAdmin"))
                    {
                        return RedirectToAction("Index", "DashBoard");
                    }
                }
                ViewBag.Error = result.Message;
                return View();
            }
            return View();
        }
        public IActionResult Signout()
        {
            userService.Sighout();
            return RedirectToAction("Login", "Account");
        }
    }
}

using AIT.DB.Models;
using AIT.DB.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    public class DashboardController : Controller
    {
        private readonly IBranchService branchService;

        public DashboardController(IBranchService branchService)
        {
            this.branchService = branchService;
        }
        public IActionResult Index()
        {
            
            
            return View(branchService.Gets());
        }
    }
}

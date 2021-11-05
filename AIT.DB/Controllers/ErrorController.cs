using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}

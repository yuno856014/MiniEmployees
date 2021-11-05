using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Models
{
    public class User : IdentityUser
    {
        public string Avatar { get; set; }
    }
}

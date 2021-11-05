using AIT.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Services
{
    public interface IBranchService
    {
        List<Branch> Gets();
        Branch Get(int BraId);
    }
}

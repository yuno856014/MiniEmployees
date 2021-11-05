using AIT.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Services
{
    public class BranchSerivce : IBranchService
    {
        private readonly DbAITContext context;

        public BranchSerivce(DbAITContext context)
        {
            this.context = context;
        }
        public Branch Get(int BraId)
        {
            return context.Branchs.FirstOrDefault(b => b.BranchId == BraId);
        }

        public List<Branch> Gets()
        {
            return context.Branchs.Include(b => b.Employees).ToList();
        }
    }
}

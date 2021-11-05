using AIT.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Create(Employee model);
        Employee Get(int EmpId);
        bool ChangeStatus(int EmpId);
        Task<Employee> Edit(Employee employee);
    }
}

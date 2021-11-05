using AIT.DB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIT.DB.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DbAITContext context;

        public EmployeeService(DbAITContext context)
        {
            this.context = context;
        }
        public bool ChangeStatus(int EmpId)
        {
            try
            {
                var employee = Get(EmpId);
                employee.Status = !employee.Status;
                context.Attach(employee);
                context.Entry(employee).State = EntityState.Modified;
                return context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Employee> Create(Employee model)
        {
            try
            {
                context.Add(model);
                var empId = await context.SaveChangesAsync();
                model.EmployeeId = empId;
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Employee> Edit(Employee employee)
        {
            try
            {
                context.Attach(employee);
                context.Entry<Employee>(employee).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return employee;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Employee Get(int EmpId)
        {
            return context.Employees.Include(e =>e.Branch).FirstOrDefault(c => c.EmployeeId == EmpId);
        }
    }
}

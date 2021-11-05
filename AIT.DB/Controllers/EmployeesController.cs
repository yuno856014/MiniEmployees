using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIT.DB.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using AIT.DB.Hellpers;
using Microsoft.AspNetCore.Hosting;
using AIT.DB.Models.Employees;
using AIT.DB.Services;
using Microsoft.AspNetCore.Authorization;

namespace AIT.DB.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    public class EmployeesController : Controller
    {
        private readonly DbAITContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEmployeeService employeeService;

        public EmployeesController(DbAITContext context,
                                    IWebHostEnvironment webHostEnvironment,
                                    IEmployeeService employeeService)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.employeeService = employeeService;
        }
        protected void SetAlert(string message, bool type)
        {
            TempData["AlertMessage"] = message;
            if (type == true)
            {
                TempData["AlertType"] = "alert alert-success alert-dismissible fade show";
            }
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var dbAITContext = _context.Employees.Include(e => e.Branch).Include(e => e.Skill);
            return View(await dbAITContext.ToListAsync());
        }
        [Route("/Employees/Inactivity")]
        public async Task<IActionResult> Inactivity()
        {
            var dbAITContext = _context.Employees.Include(e => e.Branch).Include(e => e.Skill);
            return View(await dbAITContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Branch)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branchs, "BranchId", "BranchName");
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,Dob,DateFirst,ShortIntro,BranchId,SkillId,Avatar,Status,Address")] Employee employee, IFormFile fAvt, bool type)
        {
            type = ModelState.IsValid;
            string message = string.Empty;
            if (type)
            {
                message = "Thêm Mới Nhân Viên Thành Công!";
                if (fAvt != null)
                {
                    string extension = Path.GetExtension(fAvt.FileName);
                    string Newname = Utilities.SEOUrl(employee.EmployeeName) + "preview_" + extension;
                    employee.Avatar = await Utilities.UploadFile(fAvt, @"employees\", Newname.ToLower());
                }
                else
                {
                    employee.Avatar = $"noavatar.jpg";
                }
                SetAlert(message, type);
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {

            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branchs, "BranchId", "BranchName", employee.BranchId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillName", employee.SkillId);
            return View(employee);
        }

        //POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,Dob,DateFirst,ShortIntro,BranchId,SkillId,Avatar,Status,Address")] Employee employee, IFormFile fAvt, bool type)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }
            type = ModelState.IsValid;
            string message = string.Empty;
            if (type)
            {
                message = "Chỉnh Sửa Nhân Viên Thành Công!";
                try
                {
                    string filename = employee.Avatar;
                    if (fAvt != null)
                    {
                        string extension = Path.GetExtension(fAvt.FileName);
                        string Newname = Utilities.SEOUrl(employee.EmployeeName) + "preview_" + extension;
                        employee.Avatar = await Utilities.UploadFile(fAvt, @"employees\", Newname.ToLower());
                    }
                    _context.Update(employee);
                    SetAlert(message, type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BranchId"] = new SelectList(_context.Branchs, "BranchId", "BranchName", employee.BranchId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillName", employee.SkillId);
            return View(employee);
        }
        
        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Branch)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,bool type = true)
        {
            string message = string.Empty;
            message = "Xóa nhân viên thành công";
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            SetAlert(message, type);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
        [HttpGet]
        [Route("/Employees/ChangeStatus/{empId}")]
        public IActionResult ChangeStatus(int empId,bool type = true)
        {
            string message = string.Empty;
            message = "Thay đổi trạng thái nhân viên thành công";
            ViewBag.EmployeeId = empId;
            if (employeeService.ChangeStatus(empId))
            {
                SetAlert(message, type);
                return RedirectToAction("Index");
            }
            SetAlert(message, type);
            return RedirectToAction("Index");
        }
    }
}

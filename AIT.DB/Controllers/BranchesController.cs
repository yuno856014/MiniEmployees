using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIT.DB.Models;
using AIT.DB.Services;
using Microsoft.AspNetCore.Authorization;

namespace AIT.DB.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    public class BranchesController : Controller
    {
        private readonly DbAITContext _context;
        private readonly IBranchService _branchService;

        public BranchesController(DbAITContext context,
                                  IBranchService branchService)
        {
            _context = context;
            _branchService = branchService;
        }
        protected void SetAlert(string message, bool type)
        {
            TempData["AlertMessage"] = message;
            if (type == true)
            {
                TempData["AlertType"] = "alert alert-success alert-dismissible fade show";
            }
            else
            {
                TempData["AlertType"] = "alert alert-danger alert-dismissible fade show";
            }    
        }
        // GET: Branches
        public IActionResult Index()
        {
            return View(_branchService.Gets());
        }

        // GET: Branches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branchs
                .FirstOrDefaultAsync(m => m.BranchId == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // GET: Branches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Branches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BranchId,BranchName,Address")] Branch branch, bool type)
        {
            type = ModelState.IsValid;
            string message = string.Empty;
            if (type)
            {
                message = "Thêm Mới Thành Công!";
                _context.Add(branch);
                SetAlert(message, type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branch);
        }

        // GET: Branches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branchs.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }
            return View(branch);
        }

        // POST: Branches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BranchId,BranchName,Address")] Branch branch, bool type)
        {
            if (id != branch.BranchId)
            {
                return NotFound();
            }
            type = ModelState.IsValid;
            string message = string.Empty;
            if (type)
            {
                try
                {
                    message = "Chỉnh sửa thành công!";
                    SetAlert(message, type);
                    _context.Update(branch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BranchExists(branch.BranchId))
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
            return View(branch);
        }

        // GET: Branches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branch = await _context.Branchs
                .FirstOrDefaultAsync(m => m.BranchId == id);
            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // POST: Branches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, bool type = true)
        {
            string message = string.Empty;
            var branchs = await _context.Branchs.FindAsync(id);
            try
            {
                _context.Branchs.Remove(branchs);
                message = "Xóa thành công!";
                SetAlert(message, type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                message = "Lỗi!";
                type = false;
                SetAlert(message, type);
                return View(branchs);
            }


        }

        private bool BranchExists(int id)
        {
            return _context.Branchs.Any(e => e.BranchId == id);
        }
    }
}

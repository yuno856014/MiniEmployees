using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AIT.DB.Models;
using Microsoft.AspNetCore.Authorization;

namespace AIT.DB.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    public class SkillsController : Controller
    {
        private readonly DbAITContext _context;

        public SkillsController(DbAITContext context)
        {
            _context = context;
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

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skills.ToListAsync());
        }

        // GET: Skills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .FirstOrDefaultAsync(m => m.SkillId == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // GET: Skills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Skills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SkillId,SkillName")] Skill skill,bool type)
        {
            type = ModelState.IsValid;
            string message = string.Empty;
            if (type)
            {
                message = "Thêm mới kỹ năng thành công!";
                _context.Add(skill);
                SetAlert(message, type);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skill);
        }

        // GET: Skills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SkillId,SkillName")] Skill skill,bool type)
        {
            type = ModelState.IsValid;
            string message = string.Empty;
            if (id != skill.SkillId)
            {
                return NotFound();
            }

            if (type)
            {
                try
                {
                    message = "Thay đổi kỹ năng thành công!";
                    _context.Update(skill);
                    SetAlert(message, type);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.SkillId))
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
            return View(skill);
        }

        // GET: Skills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills
                .FirstOrDefaultAsync(m => m.SkillId == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, bool type = true)
        {
            string message = string.Empty;
            var skill = await _context.Skills.FindAsync(id);
            try
            {
                message = "Xóa thành công!";
                SetAlert(message, type);
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                message = "Lỗi!";
                type = false;
                SetAlert(message, type);
                return View(skill);
            }
        }

        private bool SkillExists(int id)
        {
            return _context.Skills.Any(e => e.SkillId == id);
        }
    }
}

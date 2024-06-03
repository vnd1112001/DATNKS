using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeachingSchedulesController : Controller
    {
        private readonly UniversityManagementContext _context;

        public TeachingSchedulesController(UniversityManagementContext context)
        {
            _context = context;
        }

        // GET: Admin/TeachingSchedules
        public async Task<IActionResult> Index()
        {
            var universityManagementContext = _context.TeachingSchedules.Include(t => t.Class).Include(t => t.Subject).Include(t => t.Teacher);
            return View(await universityManagementContext.ToListAsync());
        }

        // GET: Admin/TeachingSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingSchedule = await _context.TeachingSchedules
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeachingId == id);
            if (teachingSchedule == null)
            {
                return NotFound();
            }

            return View(teachingSchedule);
        }

        // GET: Admin/TeachingSchedules/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            ViewData["TeacherId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Admin/TeachingSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeachingId,TeacherId,SubjectId,ClassId,TeachingDate,DayOfWeek,StartTime,EndTime")] TeachingSchedule teachingSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teachingSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", teachingSchedule.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", teachingSchedule.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "UserId", "UserId", teachingSchedule.TeacherId);
            return View(teachingSchedule);
        }

        // GET: Admin/TeachingSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingSchedule = await _context.TeachingSchedules.FindAsync(id);
            if (teachingSchedule == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", teachingSchedule.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", teachingSchedule.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "UserId", "UserId", teachingSchedule.TeacherId);
            return View(teachingSchedule);
        }

        // POST: Admin/TeachingSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeachingId,TeacherId,SubjectId,ClassId,TeachingDate,DayOfWeek,StartTime,EndTime")] TeachingSchedule teachingSchedule)
        {
            if (id != teachingSchedule.TeachingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teachingSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeachingScheduleExists(teachingSchedule.TeachingId))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId", teachingSchedule.ClassId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", teachingSchedule.SubjectId);
            ViewData["TeacherId"] = new SelectList(_context.Users, "UserId", "UserId", teachingSchedule.TeacherId);
            return View(teachingSchedule);
        }

        // GET: Admin/TeachingSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teachingSchedule = await _context.TeachingSchedules
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.TeachingId == id);
            if (teachingSchedule == null)
            {
                return NotFound();
            }

            return View(teachingSchedule);
        }

        // POST: Admin/TeachingSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teachingSchedule = await _context.TeachingSchedules.FindAsync(id);
            if (teachingSchedule != null)
            {
                _context.TeachingSchedules.Remove(teachingSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeachingScheduleExists(int id)
        {
            return _context.TeachingSchedules.Any(e => e.TeachingId == id);
        }
    }
}

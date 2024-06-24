using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class HolidaySchedulesController : Controller
    {
        private readonly UniversityManagementContext _context;

        public HolidaySchedulesController(UniversityManagementContext context)
        {
            _context = context;
        }

        // GET: Admin/HolidaySchedules
        public async Task<IActionResult> Index()
        {
            return View(await _context.HolidaySchedules.ToListAsync());
        }

        // GET: Admin/HolidaySchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holidaySchedule = await _context.HolidaySchedules
                .FirstOrDefaultAsync(m => m.HolidayId == id);
            if (holidaySchedule == null)
            {
                return NotFound();
            }

            return View(holidaySchedule);
        }

        // GET: Admin/HolidaySchedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/HolidaySchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HolidayId,Title,StartDate,EndDate")] HolidaySchedule holidaySchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(holidaySchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(holidaySchedule);
        }

        // GET: Admin/HolidaySchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holidaySchedule = await _context.HolidaySchedules.FindAsync(id);
            if (holidaySchedule == null)
            {
                return NotFound();
            }
            return View(holidaySchedule);
        }

        // POST: Admin/HolidaySchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HolidayId,Title,StartDate,EndDate")] HolidaySchedule holidaySchedule)
        {
            if (id != holidaySchedule.HolidayId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(holidaySchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HolidayScheduleExists(holidaySchedule.HolidayId))
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
            return View(holidaySchedule);
        }

        // GET: Admin/HolidaySchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var holidaySchedule = await _context.HolidaySchedules
                .FirstOrDefaultAsync(m => m.HolidayId == id);
            if (holidaySchedule == null)
            {
                return NotFound();
            }

            return View(holidaySchedule);
        }

        // POST: Admin/HolidaySchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var holidaySchedule = await _context.HolidaySchedules.FindAsync(id);
            if (holidaySchedule != null)
            {
                _context.HolidaySchedules.Remove(holidaySchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HolidayScheduleExists(int id)
        {
            return _context.HolidaySchedules.Any(e => e.HolidayId == id);
        }
    }
}

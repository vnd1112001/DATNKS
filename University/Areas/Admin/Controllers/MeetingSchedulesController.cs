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
    public class MeetingSchedulesController : Controller
    {
        private readonly UniversityManagementContext _context;

        public MeetingSchedulesController(UniversityManagementContext context)
        {
            _context = context;
        }

        // GET: Admin/MeetingSchedules
        public async Task<IActionResult> Index()
        {
            return View(await _context.MeetingSchedules.ToListAsync());
        }

        // GET: Admin/MeetingSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingSchedule = await _context.MeetingSchedules
                .FirstOrDefaultAsync(m => m.MeetingId == id);
            if (meetingSchedule == null)
            {
                return NotFound();
            }

            return View(meetingSchedule);
        }

        // GET: Admin/MeetingSchedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/MeetingSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MeetingId,Title,Description,MeetingDate,StartTime,EndTime")] MeetingSchedule meetingSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetingSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meetingSchedule);
        }

        // GET: Admin/MeetingSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingSchedule = await _context.MeetingSchedules.FindAsync(id);
            if (meetingSchedule == null)
            {
                return NotFound();
            }
            return View(meetingSchedule);
        }

        // POST: Admin/MeetingSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MeetingId,Title,Description,MeetingDate,StartTime,EndTime")] MeetingSchedule meetingSchedule)
        {
            if (id != meetingSchedule.MeetingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingScheduleExists(meetingSchedule.MeetingId))
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
            return View(meetingSchedule);
        }

        // GET: Admin/MeetingSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meetingSchedule = await _context.MeetingSchedules
                .FirstOrDefaultAsync(m => m.MeetingId == id);
            if (meetingSchedule == null)
            {
                return NotFound();
            }

            return View(meetingSchedule);
        }

        // POST: Admin/MeetingSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meetingSchedule = await _context.MeetingSchedules.FindAsync(id);
            if (meetingSchedule != null)
            {
                _context.MeetingSchedules.Remove(meetingSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingScheduleExists(int id)
        {
            return _context.MeetingSchedules.Any(e => e.MeetingId == id);
        }
    }
}

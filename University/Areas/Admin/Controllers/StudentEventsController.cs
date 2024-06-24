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

    public class StudentEventsController : Controller
    {
        private readonly UniversityManagementContext _context;

        public StudentEventsController(UniversityManagementContext context)
        {
            _context = context;
        }

        // GET: Admin/StudentEvents
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentEvents.ToListAsync());
        }

        // GET: Admin/StudentEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEvent = await _context.StudentEvents
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (studentEvent == null)
            {
                return NotFound();
            }

            return View(studentEvent);
        }

        // GET: Admin/StudentEvents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/StudentEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,Description,EventDate,StartTime,EndTime,Location,Organizer")] StudentEvent studentEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentEvent);
        }

        // GET: Admin/StudentEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEvent = await _context.StudentEvents.FindAsync(id);
            if (studentEvent == null)
            {
                return NotFound();
            }
            return View(studentEvent);
        }

        // POST: Admin/StudentEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,Description,EventDate,StartTime,EndTime,Location,Organizer")] StudentEvent studentEvent)
        {
            if (id != studentEvent.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentEventExists(studentEvent.EventId))
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
            return View(studentEvent);
        }

        // GET: Admin/StudentEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEvent = await _context.StudentEvents
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (studentEvent == null)
            {
                return NotFound();
            }

            return View(studentEvent);
        }

        // POST: Admin/StudentEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentEvent = await _context.StudentEvents.FindAsync(id);
            if (studentEvent != null)
            {
                _context.StudentEvents.Remove(studentEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentEventExists(int id)
        {
            return _context.StudentEvents.Any(e => e.EventId == id);
        }
    }
}

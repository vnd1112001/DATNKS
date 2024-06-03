using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Models;

namespace University.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class AnnouncementCategoriesController : Controller
    {
        private readonly UniversityManagementContext _context;

        public AnnouncementCategoriesController(UniversityManagementContext context)
        {
            _context = context;
        }

        // GET: Teacher/AnnouncementCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnnouncementCategories.ToListAsync());
        }

        // GET: Teacher/AnnouncementCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcementCategory = await _context.AnnouncementCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (announcementCategory == null)
            {
                return NotFound();
            }

            return View(announcementCategory);
        }

        // GET: Teacher/AnnouncementCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teacher/AnnouncementCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Name")] AnnouncementCategory announcementCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(announcementCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(announcementCategory);
        }

        // GET: Teacher/AnnouncementCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcementCategory = await _context.AnnouncementCategories.FindAsync(id);
            if (announcementCategory == null)
            {
                return NotFound();
            }
            return View(announcementCategory);
        }

        // POST: Teacher/AnnouncementCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Name")] AnnouncementCategory announcementCategory)
        {
            if (id != announcementCategory.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(announcementCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnnouncementCategoryExists(announcementCategory.CategoryId))
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
            return View(announcementCategory);
        }

        // GET: Teacher/AnnouncementCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var announcementCategory = await _context.AnnouncementCategories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (announcementCategory == null)
            {
                return NotFound();
            }

            return View(announcementCategory);
        }

        // POST: Teacher/AnnouncementCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var announcementCategory = await _context.AnnouncementCategories.FindAsync(id);
            if (announcementCategory != null)
            {
                _context.AnnouncementCategories.Remove(announcementCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnnouncementCategoryExists(int id)
        {
            return _context.AnnouncementCategories.Any(e => e.CategoryId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Models;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace University.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class TeachingSchedulesController : Controller
    {
        private readonly UniversityManagementContext _context;

        public TeachingSchedulesController(UniversityManagementContext context)
        {
            _context = context;
        }
		// GET: Admin/TeachingSchedules/Import
		public IActionResult Import()
		{
			return View();
		}

        // POST: Admin/TeachingSchedules/Import
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please select a valid Excel file.");
                return View();
            }

            int importedCount = 0;

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        // Log số lượng hàng trong file Excel
                        Console.WriteLine($"Row count: {rowCount}");

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var teacherId = worksheet.Cells[row, 1].Text;
                                var subjectId = worksheet.Cells[row, 2].Text;
                                var classId = worksheet.Cells[row, 3].Text;
                                var teachingDateText = worksheet.Cells[row, 4].Text;
                                var dayOfWeek = worksheet.Cells[row, 5].Text;
                                var startTimeText = worksheet.Cells[row, 6].Text;
                                var endTimeText = worksheet.Cells[row, 7].Text;

                                // Log thông tin mỗi dòng
                                Console.WriteLine($"Row {row}: TeacherId={teacherId}, SubjectId={subjectId}, ClassId={classId}, TeachingDate={teachingDateText}, DayOfWeek={dayOfWeek}, StartTime={startTimeText}, EndTime={endTimeText}");

                                if (string.IsNullOrWhiteSpace(teacherId) ||
                                    string.IsNullOrWhiteSpace(subjectId) ||
                                    string.IsNullOrWhiteSpace(classId) ||
                                    string.IsNullOrWhiteSpace(teachingDateText) ||
                                    string.IsNullOrWhiteSpace(dayOfWeek) ||
                                    string.IsNullOrWhiteSpace(startTimeText) ||
                                    string.IsNullOrWhiteSpace(endTimeText))
                                {
                                    ModelState.AddModelError(string.Empty, $"Missing data at row {row}.");
                                    continue;
                                }

                                // Xử lý định dạng ngày tháng và thời gian
                                DateTime teachingDateValue;
                                DateTime startTimeValue, endTimeValue;

                                // Xử lý định dạng ngày tháng
                                if (double.TryParse(teachingDateText, out double oaDate))
                                {
                                    teachingDateValue = DateTime.FromOADate(oaDate);
                                }
                                else if (!DateTime.TryParseExact(teachingDateText, new[] { "M/d/yyyy", "MM/dd/yyyy", "dd/MM/yyyy" }, null, System.Globalization.DateTimeStyles.None, out teachingDateValue))
                                {
                                    ModelState.AddModelError(string.Empty, $"Invalid date format at row {row}: {teachingDateText}");
                                    continue;
                                }

                                // Xử lý định dạng thời gian
                                if (!DateTime.TryParseExact(startTimeText, "h:mm tt", null, System.Globalization.DateTimeStyles.None, out startTimeValue) ||
                                    !DateTime.TryParseExact(endTimeText, "h:mm tt", null, System.Globalization.DateTimeStyles.None, out endTimeValue))
                                {
                                    ModelState.AddModelError(string.Empty, $"Invalid time format at row {row}: StartTime={startTimeText}, EndTime={endTimeText}");
                                    continue;
                                }

                                var teachingSchedule = new TeachingSchedule
                                {
                                    TeacherId = int.Parse(teacherId),
                                    SubjectId = int.Parse(subjectId),
                                    ClassId = int.Parse(classId),
                                    TeachingDate = teachingDateValue,
                                    DayOfWeek = dayOfWeek,
                                    StartTime = startTimeValue.TimeOfDay,
                                    EndTime = endTimeValue.TimeOfDay
                                };

                                _context.TeachingSchedules.Add(teachingSchedule);
                                importedCount++;
                            }
                            catch (Exception ex)
                            {
                                ModelState.AddModelError(string.Empty, $"Error at row {row}: {ex.Message}");
                            }
                        }

                        if (importedCount > 0)
                        {
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "No data was imported.");
                            return View();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();
            }

            ViewData["ImportResult"] = $"Data imported successfully. {importedCount} records added.";
            return View();
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

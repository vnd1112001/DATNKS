using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using University.Models;
using System.IO;
using OfficeOpenXml;
using Microsoft.AspNetCore.Authorization;
namespace University.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class WeeklySchedulesController : Controller
    {
        private readonly UniversityManagementContext _context;

        public WeeklySchedulesController(UniversityManagementContext context)
        {
            _context = context;
        }
        // GET: Admin/WeeklySchedules/Import
        public IActionResult Import()
        {
            return View();
        }

        // POST: Admin/WeeklySchedules/Import
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Error = "Please select a valid Excel file.";
                return View();
            }

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                        if (worksheet == null)
                        {
                            ViewBag.Error = "No worksheet found in the Excel file.";
                            return View();
                        }

                        var rowCount = worksheet.Dimension.Rows;
                        var schedules = new List<WeeklySchedule>();

                        for (int row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                var dayOfWeek = worksheet.Cells[row, 1].Text;
                                var dateText = worksheet.Cells[row, 2].Text;
                                var timeText = worksheet.Cells[row, 3].Text;
                                var content = worksheet.Cells[row, 4].Text;
                                var participants = worksheet.Cells[row, 5].Text;
                                var location = worksheet.Cells[row, 6].Text;
                                var host = worksheet.Cells[row, 7].Text;

                                if (string.IsNullOrWhiteSpace(dayOfWeek) ||
                                    string.IsNullOrWhiteSpace(dateText) ||
                                    string.IsNullOrWhiteSpace(timeText))
                                {
                                    ViewBag.Error = $"Missing data at row {row}: DayOfWeek={dayOfWeek}, Date={dateText}, Time={timeText}.";
                                    return View();
                                }

                                // Attempt to parse the date, handling numeric date formats in Excel
                                DateTime date;
                                if (!DateTime.TryParse(dateText, out date))
                                {
                                    if (double.TryParse(dateText, out double dateNumber))
                                    {
                                        date = DateTime.FromOADate(dateNumber);
                                    }
                                    else
                                    {
                                        ViewBag.Error = $"Invalid date format at row {row}: Date={dateText}.";
                                        return View();
                                    }
                                }

                                // Attempt to parse the time
                                if (!DateTime.TryParse(timeText, out var time))
                                {
                                    ViewBag.Error = $"Invalid time format at row {row}: Time={timeText}.";
                                    return View();
                                }

                                var schedule = new WeeklySchedule
                                {
                                    DayOfWeek = dayOfWeek,
                                    Date = DateOnly.FromDateTime(date),
                                    Time = TimeOnly.FromDateTime(time),
                                    Content = content,
                                    Participants = participants,
                                    Location = location,
                                    Host = host
                                };

                                schedules.Add(schedule);
                            }
                            catch (Exception ex)
                            {
                                ViewBag.Error = $"Error at row {row}: {ex.Message}";
                                return View();
                            }
                        }

                        if (schedules.Any())
                        {
                            _context.WeeklySchedules.AddRange(schedules);
                            await _context.SaveChangesAsync();
                            ViewBag.Message = "Data imported successfully.";
                        }
                        else
                        {
                            ViewBag.Error = "No data was imported.";
                        }

                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error occurred while importing data: {ex.Message}";
                return View();
            }
        }

        // GET: Admin/WeeklySchedules
        public async Task<IActionResult> Index()
        {
            return View(await _context.WeeklySchedules.ToListAsync());
        }

        // GET: Admin/WeeklySchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weeklySchedule = await _context.WeeklySchedules
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (weeklySchedule == null)
            {
                return NotFound();
            }

            return View(weeklySchedule);
        }

        // GET: Admin/WeeklySchedules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/WeeklySchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,DayOfWeek,Date,Time,Content,Participants,Location,Host")] WeeklySchedule weeklySchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(weeklySchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weeklySchedule);
        }

        // GET: Admin/WeeklySchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weeklySchedule = await _context.WeeklySchedules.FindAsync(id);
            if (weeklySchedule == null)
            {
                return NotFound();
            }
            return View(weeklySchedule);
        }

        // POST: Admin/WeeklySchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,DayOfWeek,Date,Time,Content,Participants,Location,Host")] WeeklySchedule weeklySchedule)
        {
            if (id != weeklySchedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weeklySchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeeklyScheduleExists(weeklySchedule.ScheduleId))
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
            return View(weeklySchedule);
        }

        // GET: Admin/WeeklySchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weeklySchedule = await _context.WeeklySchedules
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (weeklySchedule == null)
            {
                return NotFound();
            }

            return View(weeklySchedule);
        }

        // POST: Admin/WeeklySchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weeklySchedule = await _context.WeeklySchedules.FindAsync(id);
            if (weeklySchedule != null)
            {
                _context.WeeklySchedules.Remove(weeklySchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeeklyScheduleExists(int id)
        {
            return _context.WeeklySchedules.Any(e => e.ScheduleId == id);
        }
    }
}

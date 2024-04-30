using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;

namespace CMS.Controllers
{
    public class CourseSessionalTablesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseSessionalTablesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseSessionalTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseSessionalTables.ToListAsync());
        }

        // GET: CourseSessionalTables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSessionalTable = await _context.CourseSessionalTables
                .FirstOrDefaultAsync(m => m.SessionalID == id);
            if (courseSessionalTable == null)
            {
                return NotFound();
            }

            return View(courseSessionalTable);
        }

        // GET: CourseSessionalTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseSessionalTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SessionalID,CourseRegistrationID,SessionalName,ObtainedMarks,TotalMarks,Weightage")] CourseSessionalTable courseSessionalTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseSessionalTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseSessionalTable);
        }

        // GET: CourseSessionalTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSessionalTable = await _context.CourseSessionalTables.FindAsync(id);
            if (courseSessionalTable == null)
            {
                return NotFound();
            }
            return View(courseSessionalTable);
        }

        // POST: CourseSessionalTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionalID,CourseRegistrationID,SessionalName,ObtainedMarks,TotalMarks,Weightage")] CourseSessionalTable courseSessionalTable)
        {
            if (id != courseSessionalTable.SessionalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseSessionalTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseSessionalTableExists(courseSessionalTable.SessionalID))
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
            return View(courseSessionalTable);
        }

        // GET: CourseSessionalTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSessionalTable = await _context.CourseSessionalTables
                .FirstOrDefaultAsync(m => m.SessionalID == id);
            if (courseSessionalTable == null)
            {
                return NotFound();
            }

            return View(courseSessionalTable);
        }

        // POST: CourseSessionalTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSessionalTable = await _context.CourseSessionalTables.FindAsync(id);
            if (courseSessionalTable != null)
            {
                _context.CourseSessionalTables.Remove(courseSessionalTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseSessionalTableExists(int id)
        {
            return _context.CourseSessionalTables.Any(e => e.SessionalID == id);
        }
    }
}

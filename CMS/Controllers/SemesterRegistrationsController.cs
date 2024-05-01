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
    public class SemesterRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SemesterRegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SemesterRegistrations
        public async Task<IActionResult> Index()
        {
            return View(await _context.SemesterRegistrations.ToListAsync());
        }

        // GET: SemesterRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semesterRegistration = await _context.SemesterRegistrations
                .FirstOrDefaultAsync(m => m.SemesterRegistrationID == id);
            if (semesterRegistration == null)
            {
                return NotFound();
            }

            return View(semesterRegistration);
        }

        // GET: SemesterRegistrations/Create
        public IActionResult Create()
        {
            ViewData["Students"] = new SelectList(_context.Students, "StudentID", "FirstName");
            ViewData["Sessions"] = new SelectList(_context.Sessions, "SessionID", "SessionName");
            return View();
        }

        // POST: SemesterRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SemesterRegistrationID,StudentID,SemesterNumber,SessionID")] SemesterRegistration semesterRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(semesterRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(semesterRegistration);
        }

        // GET: SemesterRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semesterRegistration = await _context.SemesterRegistrations.FindAsync(id);
            if (semesterRegistration == null)
            {
                return NotFound();
            }

            ViewData["Students"] = new SelectList(_context.Students, "StudentID", "FirstName", semesterRegistration.StudentID);
            ViewData["Sessions"] = new SelectList(_context.Sessions, "SessionID", "SessionName", semesterRegistration.SessionID);

            return View(semesterRegistration);
        }

        // POST: SemesterRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SemesterRegistrationID,StudentID,SemesterNumber,SessionID")] SemesterRegistration semesterRegistration)
        {
            if (id != semesterRegistration.SemesterRegistrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(semesterRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SemesterRegistrationExists(semesterRegistration.SemesterRegistrationID))
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
            return View(semesterRegistration);
        }

        // GET: SemesterRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semesterRegistration = await _context.SemesterRegistrations
                .FirstOrDefaultAsync(m => m.SemesterRegistrationID == id);
            if (semesterRegistration == null)
            {
                return NotFound();
            }

            return View(semesterRegistration);
        }

        // POST: SemesterRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var semesterRegistration = await _context.SemesterRegistrations.FindAsync(id);
            if (semesterRegistration != null)
            {
                _context.SemesterRegistrations.Remove(semesterRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SemesterRegistrationExists(int id)
        {
            return _context.SemesterRegistrations.Any(e => e.SemesterRegistrationID == id);
        }
    }
}

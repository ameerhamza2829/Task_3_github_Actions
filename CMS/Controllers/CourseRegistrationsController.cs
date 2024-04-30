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
    public class CourseRegistrationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseRegistrationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseRegistrations
        public async Task<IActionResult> Index()
        {
            return View(await _context.CourseRegistrations.ToListAsync());
        }

        // GET: CourseRegistrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseRegistration = await _context.CourseRegistrations
                .FirstOrDefaultAsync(m => m.CourseRegistrationID == id);
            if (courseRegistration == null)
            {
                return NotFound();
            }

            return View(courseRegistration);
        }

        // GET: CourseRegistrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseRegistrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseRegistrationID,SemesterRegistrationID,CourseID,SessionID,TeacherID,MidPercentage,FinalPercentage,SessionalPercentage")] CourseRegistration courseRegistration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseRegistration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseRegistration);
        }

        // GET: CourseRegistrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseRegistration = await _context.CourseRegistrations.FindAsync(id);
            if (courseRegistration == null)
            {
                return NotFound();
            }
            return View(courseRegistration);
        }

        // POST: CourseRegistrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseRegistrationID,SemesterRegistrationID,CourseID,SessionID,TeacherID,MidPercentage,FinalPercentage,SessionalPercentage")] CourseRegistration courseRegistration)
        {
            if (id != courseRegistration.CourseRegistrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseRegistration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseRegistrationExists(courseRegistration.CourseRegistrationID))
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
            return View(courseRegistration);
        }

        // GET: CourseRegistrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseRegistration = await _context.CourseRegistrations
                .FirstOrDefaultAsync(m => m.CourseRegistrationID == id);
            if (courseRegistration == null)
            {
                return NotFound();
            }

            return View(courseRegistration);
        }

        // POST: CourseRegistrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseRegistration = await _context.CourseRegistrations.FindAsync(id);
            if (courseRegistration != null)
            {
                _context.CourseRegistrations.Remove(courseRegistration);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseRegistrationExists(int id)
        {
            return _context.CourseRegistrations.Any(e => e.CourseRegistrationID == id);
        }
    }
}

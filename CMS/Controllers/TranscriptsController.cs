using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;
using CMS.Utility;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers
{
    [Authorize(SD.Role_Admin)]
    public class TranscriptsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TranscriptsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Transcripts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Transcripts.ToListAsync());
        }

        // GET: Transcripts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcript = await _context.Transcripts
                .FirstOrDefaultAsync(m => m.TranscriptID == id);
            if (transcript == null)
            {
                return NotFound();
            }

            return View(transcript);
        }

        // GET: Transcripts/Create
        public IActionResult Create()
        {
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View();
        }

        // POST: Transcripts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TranscriptID,StudentID,SemesterNumber,CourseID,CourseGrade,SemesterGrade")] Transcript transcript)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transcript);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transcript);
        }

        // GET: Transcripts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcript = await _context.Transcripts.FindAsync(id);
            if (transcript == null)
            {
                return NotFound();
            }
            ViewBag.Students = _context.Students.ToList();
            ViewBag.Courses = _context.Courses.ToList();
            return View(transcript);
        }

        // POST: Transcripts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TranscriptID,StudentID,SemesterNumber,CourseID,CourseGrade,SemesterGrade")] Transcript transcript)
        {
            if (id != transcript.TranscriptID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transcript);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranscriptExists(transcript.TranscriptID))
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
            return View(transcript);
        }

        // GET: Transcripts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transcript = await _context.Transcripts
                .FirstOrDefaultAsync(m => m.TranscriptID == id);
            if (transcript == null)
            {
                return NotFound();
            }

            return View(transcript);
        }

        // POST: Transcripts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transcript = await _context.Transcripts.FindAsync(id);
            if (transcript != null)
            {
                _context.Transcripts.Remove(transcript);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TranscriptExists(int id)
        {
            return _context.Transcripts.Any(e => e.TranscriptID == id);
        }
    }
}

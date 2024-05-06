using CMS.Data;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class BatchSectionSemesterController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public BatchSectionSemesterController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: BatchSectionSemesterController
        public ActionResult Index()
        {
            // Retrieve the students grouped by department, campus, batch, and session
            var groupedStudents = _context.Students
    .GroupBy(s => new { s.DepartmentID, s.CampusID, s.BatchID, s.SectionID })
    .Select(g => new StudentGroupViewModel
    {
        DepartmentName = _context.Departments.FirstOrDefault(d => d.DepartmentID == g.Key.DepartmentID).DepartmentName,
        SectionName = _context.Sections.FirstOrDefault(s => s.SectionID == g.Key.SectionID).SectionName,
        CampusName = _context.Campuses.FirstOrDefault(c => c.CampusID == g.Key.CampusID).CampusName,
        BatchName = _context.Batches.FirstOrDefault(b => b.BatchID == g.Key.BatchID).BatchName,
        Students = g.ToList()
    })
    .ToList();
            Console.WriteLine(groupedStudents);
            return View(groupedStudents);
        }

    }
}

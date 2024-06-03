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
        public ActionResult Index()
        {
            var campuses = _context.Campuses
                .Select(c => new CampusViewModel
                {
                    CampusId = c.CampusID,
                    CampusName = c.CampusName,
                    Departments = _context.Departments
                        .Where(d => _context.Students.Any(s => s.CampusID == c.CampusID && s.DepartmentID == d.DepartmentID))
                        .Select(d => new DepartmentViewModel
                        {
                            DepartmentId = d.DepartmentID,
                            DepartmentName = d.DepartmentName,
                            Batches = _context.Batches
                                .Where(b => _context.Students.Any(s => s.CampusID == c.CampusID && s.DepartmentID == d.DepartmentID && s.BatchID == b.BatchID))
                                .Select(b => new BatchViewModel
                                {
                                    BatchId = b.BatchID,
                                    BatchName = b.BatchName,
                                    Sections = _context.Sections
                                        .Where(s => _context.Students.Any(st => st.CampusID == c.CampusID && st.DepartmentID == d.DepartmentID && st.BatchID == b.BatchID && st.SectionID == s.SectionID))
                                        .Select(s => new SectionViewModel
                                        {
                                            SectionId = s.SectionID,
                                            SectionName = s.SectionName,
                                            Students = _context.Students
                                                .Where(st => st.CampusID == c.CampusID && st.DepartmentID == d.DepartmentID && st.BatchID == b.BatchID && st.SectionID == s.SectionID)
                                                .Select(st => new StudentViewModel
                                                {
                                                    RollNo = st.RollNo,
                                                    FirstName = st.FirstName,
                                                    LastName = st.LastName
                                                })
                                                .ToList()
                                        })
                                        .ToList()
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

            var viewModel = new UniversityViewModel
            {
                Campuses = campuses
            };

            return View(viewModel);
        }
        public IActionResult ViewStudents(int campusId, int departmentId, int batchId, int sectionId)
        {
            var students = _context.Students
                .Where(st => st.CampusID == campusId && st.DepartmentID == departmentId && st.BatchID == batchId && st.SectionID == sectionId)
                .Select(st => new StudentViewModel
                {
                    RollNo = st.RollNo,
                    FirstName = st.FirstName,
                    LastName = st.LastName
                })
                .ToList();

            var campus = _context.Campuses
                .Where(c => c.CampusID == campusId)
                .Select(c => c.CampusName)
                .SingleOrDefault(); // Retrieves the single CampusName

            var department = _context.Departments
                .Where(d => d.DepartmentID == departmentId)
                .Select(d => d.DepartmentName)
                .SingleOrDefault(); // Retrieves the single DepartmentName

            var batch = _context.Batches
                .Where(b => b.BatchID == batchId)
                .Select(b => b.BatchName)
                .SingleOrDefault(); // Retrieves the single BatchName

            var section = _context.Sections
                .Where(s => s.SectionID == sectionId)
                .Select(s => s.SectionName) // Assuming you meant SectionName instead of SectionID
                .SingleOrDefault(); // Retrieves the single SectionName

            var viewModel = new StudentsListViewModel
            {
                Students = students,
                CampusName = campus,
                DepartmentName = department,
                BatchName = batch,
                SectionName = section
            };

            return View(viewModel);
        }



    }
}

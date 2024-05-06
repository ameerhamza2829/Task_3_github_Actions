using CMS.Models;

namespace CMS.ViewModels
{
    public class StudentGroupViewModel
    {
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string CampusName { get; set; }
        public string BatchName { get; set; }
        public List<Student> Students { get; set; }
    }
}
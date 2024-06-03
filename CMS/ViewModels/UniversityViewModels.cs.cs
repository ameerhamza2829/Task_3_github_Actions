namespace CMS.ViewModels
{
    public class StudentViewModel
    {
        public string RollNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SectionViewModel
    {
        public int SectionId { get; set; } // Add SectionId property
        public string SectionName { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }

    public class BatchViewModel
    {
        public int BatchId { get; set; } // Add BatchId property
        public string BatchName { get; set; }
        public List<SectionViewModel> Sections { get; set; }
    }

    public class DepartmentViewModel
    {
        public int DepartmentId { get; set; } // Add DepartmentId property
        public string DepartmentName { get; set; }
        public List<BatchViewModel> Batches { get; set; }
    }

    public class CampusViewModel
    {
        public int CampusId { get; set; } // Add CampusId property
        public string CampusName { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
    }

    public class UniversityViewModel
    {
        public List<CampusViewModel> Campuses { get; set; }
    }
    public class StudentsListViewModel
    {
        public List<StudentViewModel> Students { get; set; }
        public string CampusName { get; set; }
        public string DepartmentName { get; set; }
        public string BatchName { get; set; }
        public string SectionName { get; set; }
    }
}

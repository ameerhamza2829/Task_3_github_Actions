using CMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CMS.Data
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Transcript> Transcripts { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<CourseRegistration> CourseRegistrations { get; set; }
        public DbSet<CourseSessionalTable> CourseSessionalTables { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SemesterRegistration> SemesterRegistrations { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Grades> Grades { get; set; }
    }
}

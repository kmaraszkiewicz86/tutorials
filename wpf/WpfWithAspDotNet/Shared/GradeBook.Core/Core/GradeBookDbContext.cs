using System.Data.Entity;
using GradeBook.Core.Entities;

namespace GradeBook.Core.Core
{
    public class GradeBookDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<SchoolSubject> SchoolSubjects { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<SchoolSubjectStudent> SchoolSubjectStudents { get; set; }

        public GradeBookDbContext() : base("GradeBook")
        {

        }
    }
}

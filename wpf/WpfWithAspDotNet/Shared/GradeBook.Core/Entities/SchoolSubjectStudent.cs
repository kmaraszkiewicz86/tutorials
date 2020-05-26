using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Core.Entities
{
    public class SchoolSubjectStudent
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        [Required]
        [Index("Unq_SchoolSubjectStudent_Student_SchoolSubject", 1, IsUnique = true)]
        public Student Student { get; set; }

        public int SchoolSubjectId { get; set; }

        [Required]
        [Index("Unq_SchoolSubjectStudent_Student_SchoolSubject", 2, IsUnique = true)]
        public SchoolSubject SchoolSubject { get; set; }
    }
}

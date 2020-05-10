using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Core.Entities
{
    public class SchoolSubject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<SchoolSubjectStudent> SchoolSubjectStudents { get; set; }

        public IEnumerable<Grade> Grades { get; set; }
    }
}

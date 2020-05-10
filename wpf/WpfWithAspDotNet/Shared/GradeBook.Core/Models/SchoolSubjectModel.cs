using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Core.Models
{
    public class SchoolSubjectModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public IEnumerable<StudentModel> SchoolSubjectStudents { get; set; }

        public IEnumerable<GradeModel> Grades { get; set; }
    }
}

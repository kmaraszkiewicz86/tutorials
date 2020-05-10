using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Core.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("Unq_Student_Name_Lastname", 1, IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(150)]
        [Index("Unq_Student_Name_Lastname", 2, IsUnique = true)]
        public string Lastname { get; set; }

        public IEnumerable<SchoolSubjectStudent> SchoolSubjectStudents { get; set; }

        public IEnumerable<Grade> Grades { get; set; }
    }
}

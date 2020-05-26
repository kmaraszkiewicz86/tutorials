using System.ComponentModel.DataAnnotations;

namespace GradeBook.Core.Models
{
    public class GradeModel
    {
        public int Id { get; set; }

        public int SchoolSubjectId { get; set; }

        [Required]
        public SchoolSubjectModel SchoolSubject { get; set; }

        public int StudentId { get; set; }

        [Required]
        public StudentModel Student { get; set; }

        [Required]
        [Range(2, 6)]
        public int GradeValue { get; set; }
    }
}

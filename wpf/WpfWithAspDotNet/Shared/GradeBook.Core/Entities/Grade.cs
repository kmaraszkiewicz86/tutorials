using System.ComponentModel.DataAnnotations;

namespace GradeBook.Core.Entities
{
    public class Grade
    {
        public int Id { get; set; }

        public int SchoolSubjectId { get; set; }

        [Required]
        public SchoolSubject SchoolSubject { get; set; }

        public int StudentId { get; set; }

        [Required]
        public Student Student { get; set; }

        [Required]
        [Range(2, 6)]
        public int GradeValue { get; set; }
    }
}

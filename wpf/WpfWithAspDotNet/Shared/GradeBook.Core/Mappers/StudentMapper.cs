using System.Collections.Generic;
using System.Linq;
using GradeBook.Core.Entities;
using GradeBook.Core.Models;

namespace GradeBook.Core.Mappers
{
    public static class StudentMapper
    {
        public static StudentModel MapToStudentModel(this Student student)
        {
            return new StudentModel
            {
                Id = student.Id,
                Name = student.Name,
                Lastname = student.Lastname
            };
        }

        public static IEnumerable<StudentModel> MapToStudentModels(this IEnumerable<Student> students)
        {
            return students.Select(s => s.MapToStudentModel());
        }
    }
}

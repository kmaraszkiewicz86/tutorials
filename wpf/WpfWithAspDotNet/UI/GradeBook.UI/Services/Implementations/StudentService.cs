using System.Collections.Generic;
using GradeBook.Core.Models;
using GradeBook.UI.Services.Interfaces;

namespace GradeBook.UI.Services.Implementations
{
    public class StudentService: IStudentService
    {
        public IEnumerable<StudentModel> GetAll()
        {
            return new List<StudentModel>
            {
                new StudentModel
                {
                    Id = 1,
                    Name = "test",
                    Lastname = "test"
                }
            };
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.Core.Models;
using GradeBook.Core.Services.Interfaces;

namespace GradeBook.Core.Services.Implementations
{
    public class StudentDbService : IStudentDbService
    {
        public IEnumerable<StudentModel> GetAll()
        {
            return new List<StudentModel>
            {
                new StudentModel
                {
                    Id =1,
                    Lastname = "test",
                    Name = "test"
                }
            };
        }

        public Task InsertNew(StudentModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}

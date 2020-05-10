using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.Core.Core;
using GradeBook.Core.Mappers;
using GradeBook.Core.Models;
using GradeBook.Core.Services.Interfaces;

namespace GradeBook.Core.Services.Implementations
{
    public class StudentDbService : IStudentDbService
    {
        private GradeBookDbContext _bookDbContext;

        public StudentDbService(GradeBookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }

        public IEnumerable<StudentModel> GetAll()
        {
            return _bookDbContext.Students.MapToStudentModels();
        }

        public Task InsertNew(StudentModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}

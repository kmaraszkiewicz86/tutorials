using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.Core.Models;

namespace GradeBook.Core.Services.Interfaces
{
    public interface IStudentDbService
    {
        Task InsertNew(StudentModel model);

        IEnumerable<StudentModel> GetAll();
    }
}

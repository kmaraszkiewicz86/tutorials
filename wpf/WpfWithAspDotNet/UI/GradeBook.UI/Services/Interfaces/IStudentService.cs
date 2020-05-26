using System.Collections.Generic;
using GradeBook.Core.Models;

namespace GradeBook.UI.Services.Interfaces
{
    public interface IStudentService
    {
        IEnumerable<StudentModel> GetAll();
    }
}

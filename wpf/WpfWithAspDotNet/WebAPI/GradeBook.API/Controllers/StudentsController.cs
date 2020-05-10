using System.Collections.Generic;
using System.Web.Http;
using GradeBook.Core.Models;
using GradeBook.Core.Services.Interfaces;

namespace GradeBook.API.Controllers
{
    [Route("api/StudentList")]
    public class StudentsController : ApiController
    {
        private IStudentDbService _studentDbService;

        public StudentsController(IStudentDbService studentDbService)
        {
            _studentDbService = studentDbService;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<IEnumerable<StudentModel>>(_studentDbService.GetAll());
        }
    }
}

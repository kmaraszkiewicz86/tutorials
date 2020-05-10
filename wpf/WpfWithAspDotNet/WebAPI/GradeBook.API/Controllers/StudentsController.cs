using System.Web.Http;

namespace GradeBook.API.Controllers
{
    [Route("api/StudentList")]
    public class StudentsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<string[]>(new string[] { "value1", "value2" });
        }
    }
}

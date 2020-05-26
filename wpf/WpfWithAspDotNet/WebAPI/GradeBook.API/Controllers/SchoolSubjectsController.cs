using System.Web.Http;

namespace GradeBook.API.Controllers
{
    [Route("api/SchoolSubjects")]
    public class SchoolSubjectsController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<string[]>(new string[] { "value1", "value2" });
        }
    }
}

using System.Web.Http;

namespace GradeBook.API.Controllers
{
    [Route("api/Grades")]
    public class GradesController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok<string[]>(new string[] { "value1", "value2" });
        }
    }
}

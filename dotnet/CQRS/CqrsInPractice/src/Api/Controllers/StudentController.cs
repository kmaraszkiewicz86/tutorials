using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Students;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly Messages _messages;

        public StudentController(Messages messages)
        {
            _messages = messages;
        }

        [HttpGet]
        public IActionResult GetList(string enrolled, int? number)
        {
            var query = new GetListQuery(enrolled, number);

            return Ok(_messages.Dispatch(query));
        }

        [HttpPost]
        public IActionResult Register([FromBody] NewStudentDto dto)
        {
            Result result = _messages.Dispatch(new RegisterCommand(dto));

            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Unregister(long id)
        {
            Result result = _messages.Dispatch(new UnregisterCommand(id));

            return FromResult(result);
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, [FromBody] StudentEnrollmentDto dto)
        {
            Result result = _messages.Dispatch(new EnrollCommand(id, dto.Course, dto.Grade));

            return FromResult(result);
        }

        [HttpPost("{id}/enrollments/{enrollmentNumber}/deletion")]
        public IActionResult Disenroll(long id, int enrollmentNumber,
            [FromBody] StudentDisenrollmentDto dto)
        {
            Result result = _messages.Dispatch(new DisenrollCommand(id, enrollmentNumber, dto.Comment));

            return FromResult(result);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(long id, [FromBody] StudentPersonalInfoDto dto)
        {
            Result result = _messages.Dispatch(new EditPersonalInfoCommand(id, dto.Name, dto.Email));

            return FromResult(result);
        }

        [HttpPut("{id}/enrollments/{enrollmentNumber}")]
        public IActionResult Transfer(long id, int enrollmentNumber, [FromBody] StudentTransferDto dto)
        {
            Result result = _messages.Dispatch(new TransferCommand(id, enrollmentNumber, dto.Course, dto.Grade));

            return FromResult(result);
        }
    }
}
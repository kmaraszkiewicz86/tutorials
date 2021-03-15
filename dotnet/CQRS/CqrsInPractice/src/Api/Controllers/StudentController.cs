using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly UnitOfWork _unitOfWork;
        private readonly StudentRepository _studentRepository;
        private readonly CourseRepository _courseRepository;
        private readonly Messages _messages;

        public StudentController(UnitOfWork unitOfWork, Messages messages)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = new StudentRepository(unitOfWork);
            _courseRepository = new CourseRepository(unitOfWork);
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

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpDelete("{id}")]
        public IActionResult Unregister(long id)
        {
            Result result = _messages.Dispatch(new UnregisterCommand(id));

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, [FromBody] StudentEnrollmentDto dto)
        {
            Result result = _messages.Dispatch(new EnrollCommand(id, dto.Course, dto.Grade));

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPost("{id}/enrollments/{enrollmentNumber}/deletion")]
        public IActionResult Disenroll(long id, int enrollmentNumber,
            [FromBody] StudentDisenrollmentDto dto)
        {
            Result result = _messages.Dispatch(new DisenrollCommand(id, enrollmentNumber, dto.Comment));

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(long id, [FromBody] StudentPersonalInfoDto dto)
        {
            Result result = _messages.Dispatch(new EditPersonalInfoCommand(id, dto.Name, dto.Email));

            return result.IsSuccess ? Ok() : Error(result.Error);
        }

        [HttpPut("{id}/enrollments/{enrollmentNumber}")]
        public IActionResult Transfer(long id, int enrollmentNumber, [FromBody] StudentTransferDto dto)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
                return Error($"No student found for Id {id}");

            Course course = _courseRepository.GetByName(dto.Course);

            if (course == null)
                return Error($"Course is incorrect: '{dto.Course}'");

            var success = Enum.TryParse(dto.Grade, out Grade grade);
            if (!success)
                return Error($"Grade is incorrect: {dto.Grade}");

            Enrollment enrollment = student.GetEnrollment(enrollmentNumber);

            if (enrollment == null)
                return Error($"No enrollment found with the number: '{enrollmentNumber}'");

            enrollment.Update(course, grade);

            _unitOfWork.Commit();

            return Ok();
        }

        private bool HasEnrollmentChanged(string newCourseName, string newGrade, Enrollment enrollment)
        {
            if (string.IsNullOrWhiteSpace(newCourseName) && enrollment == null)
                return false;

            if (string.IsNullOrWhiteSpace(newCourseName) || enrollment == null)
                return true;

            return newCourseName != enrollment.Course.Name || newGrade != enrollment.Grade.ToString();
        }
    }
}

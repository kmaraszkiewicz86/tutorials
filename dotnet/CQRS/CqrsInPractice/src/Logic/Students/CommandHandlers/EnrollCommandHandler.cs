using System;
using CSharpFunctionalExtensions;
using Logic.Decorators;
using Logic.Students.Commands;
using Logic.Utils;

namespace Logic.Students.CommandHandlers
{
    [DatabaseRetry]
    [AuditLogging]
    public sealed class EnrollCommandHandler : ICommandHandler<EnrollCommand>
    {
        private readonly SessionFactory _sessionFactory;

        public EnrollCommandHandler(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public Result Handle(EnrollCommand command)
        {
            var unitOfWork = new UnitOfWork(_sessionFactory);

            var studentRepository = new StudentRepository(unitOfWork);
            var courseRepository = new CourseRepository(unitOfWork);

            Student student = studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            Course course = courseRepository.GetByName(command.Course);

            if (course == null)
                return Result.Fail($"Course is incorrect: '{command.Course}'");

            var success = Enum.TryParse(command.Grade, out Grade grade);
            if (!success)
                return Result.Fail($"Grade is incorrect: {command.Grade}");

            student.Enroll(course, grade);

            unitOfWork.Commit();

            return Result.Ok();
        }
    }
}

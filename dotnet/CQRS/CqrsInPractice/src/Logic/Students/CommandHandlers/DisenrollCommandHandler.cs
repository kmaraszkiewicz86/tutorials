using CSharpFunctionalExtensions;
using Logic.Decorators;
using Logic.Students.Commands;
using Logic.Utils;

namespace Logic.Students.CommandHandlers
{
    [DatabaseRetry]
    [AuditLogging]
    public sealed class DisenrollCommandHandler : ICommandHandler<DisenrollCommand>
    {
        private readonly SessionFactory _sessionFactory;

        public DisenrollCommandHandler(SessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public Result Handle(DisenrollCommand command)
        {
            var unitOfWork = new UnitOfWork(_sessionFactory);

            var studentRepository = new StudentRepository(unitOfWork);
            var courseRepository = new CourseRepository(unitOfWork);

            Student student = studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            if (string.IsNullOrWhiteSpace(command.Comment))
                return Result.Fail("Disenrollment comment is required");

            Enrollment enrollment = student.GetEnrollment(command.EnrollmentNumber);

            if (enrollment == null)
                return Result.Fail($"No enrollment found with number '{command.EnrollmentNumber}'");

            student.RemoveEnrollment(enrollment, command.Comment);

            unitOfWork.Commit();

            return Result.Ok();
        }
    }
}

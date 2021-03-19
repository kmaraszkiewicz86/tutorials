using System;
using CSharpFunctionalExtensions;
using Logic.Decorators;
using Logic.Dtos;
using Logic.Students;
using Logic.Utils;

namespace Logic.AppServices.Commands
{
    public sealed class RegisterCommand : ICommand
    {
        public string Name { get; }
        public string Email { get; }

        public string Course1 { get; }
        public string Course1Grade { get; }

        public string Course2 { get; }
        public string Course2Grade { get; }

        public RegisterCommand(NewStudentDto newStudentDto)
        {
            Name = newStudentDto.Name;
            Email = newStudentDto.Email;
            Course1 = newStudentDto.Course1;
            Course1Grade = newStudentDto.Course1Grade;
            Course2 = newStudentDto.Course2;
            Course2Grade = newStudentDto.Course2Grade;
        }

        [AuditLogging]
        [DatabaseRetry]
        internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
        {
            private readonly SessionFactory _sessionFactory;

            public RegisterCommandHandler(SessionFactory sessionFactory)
            {
                _sessionFactory = sessionFactory;
            }

            public Result Handle(RegisterCommand command)
            {
                var unitOfWork = new UnitOfWork(_sessionFactory);

                var student = new Student(command.Name, command.Email);

                var studentRepository = new StudentRepository(unitOfWork);
                var courseRepository = new CourseRepository(unitOfWork);

                if (command.Course1 != null && command.Course1Grade != null)
                {
                    Course course = courseRepository.GetByName(command.Course1);
                    student.Enroll(course, Enum.Parse<Grade>(command.Course1Grade));
                }

                if (command.Course2 != null && command.Course2Grade != null)
                {
                    Course course = courseRepository.GetByName(command.Course2);
                    student.Enroll(course, Enum.Parse<Grade>(command.Course2Grade));
                }

                studentRepository.Save(student);
                unitOfWork.Commit();

                return Result.Ok();
            }
        }
    }
}
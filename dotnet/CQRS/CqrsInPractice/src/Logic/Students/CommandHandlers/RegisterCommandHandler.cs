using System;
using CSharpFunctionalExtensions;
using Logic.Students.Commands;
using Logic.Utils;

namespace Logic.Students.CommandHandlers
{
    public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly UnitOfWork _unitOfWork;

        public RegisterCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result Handle(RegisterCommand command)
        {
            var student = new Student(command.Name, command.Email);

            var studentRepository = new StudentRepository(_unitOfWork);
            var courseRepository = new CourseRepository(_unitOfWork);

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
            _unitOfWork.Commit();

            return Result.Ok();
        }
    }
}

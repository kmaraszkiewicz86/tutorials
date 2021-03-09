using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Utils;

namespace Logic.Students
{
    public interface ICommand
    {

    }

    public interface IQuery<TResult>
    {

    }

    public sealed class EditPersonalInfoCommand : ICommand
    {
        public long Id { get; }

        public string Name { get; }

        public string Email { get; }

        public EditPersonalInfoCommand(long id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
    }

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }

    public interface IQueryHandler<TQuery, TResult> : IQuery<TResult>
    {
        TResult Handle(TQuery command);
    }

    public sealed class GetListQuery : IQuery<List<StudentDto>>
    {
        public string Enrolled { get; }

        public int? EnrollmentNumber { get; }

        public GetListQuery(string enrolled, int? enrollmentNumber)
        {
            Enrolled = enrolled;
            EnrollmentNumber = enrollmentNumber;
        }
    }

    public class GetListQueryHandler : IQueryHandler<GetListQuery, List<StudentDto>>
    {
        private readonly UnitOfWork _unitOfWork;

        public GetListQueryHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<StudentDto> Handle(GetListQuery command)
        {
            var studentRepository = new StudentRepository(_unitOfWork);

            return studentRepository
                .GetList(command.Enrolled, command.EnrollmentNumber)
                .Select(x => ConvertToDto(x)).ToList()
                .ToList();
        }

        private StudentDto ConvertToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Course1 = student.FirstEnrollment?.Course?.Name,
                Course1Grade = student.FirstEnrollment?.Grade.ToString(),
                Course1Credits = student.FirstEnrollment?.Course?.Credits,
                Course2 = student.SecondEnrollment?.Course?.Name,
                Course2Grade = student.SecondEnrollment?.Grade.ToString(),
                Course2Credits = student.SecondEnrollment?.Course?.Credits,
            };
        }
    }

    public sealed class EditPersonalInfoCommandHandler : ICommandHandler<EditPersonalInfoCommand>
    {
        private readonly UnitOfWork _unitOfWork;

        public EditPersonalInfoCommandHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result Handle(EditPersonalInfoCommand command)
        {
            var studentRepository = new StudentRepository(_unitOfWork);

            Student student = studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            _unitOfWork.Commit();

            return Result.Ok();
        }
    }
}

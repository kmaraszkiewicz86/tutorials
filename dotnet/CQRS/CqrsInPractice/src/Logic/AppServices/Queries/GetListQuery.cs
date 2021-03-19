using System.Collections.Generic;
using System.Linq;
using Logic.Decorators;
using Logic.Dtos;
using Logic.Students;
using Logic.Utils;

namespace Logic.AppServices.Queries
{
    public sealed class GetListQuery : IQuery<List<StudentDto>>
    {
        public string Enrolled { get; }

        public int? EnrollmentNumber { get; }

        public GetListQuery(string enrolled, int? enrollmentNumber)
        {
            Enrolled = enrolled;
            EnrollmentNumber = enrollmentNumber;
        }

        internal class GetListQueryHandler : IQueryHandler<GetListQuery, List<StudentDto>>
        {
            private readonly SessionFactory _sessionFactory;

            public GetListQueryHandler(SessionFactory sessionFactory)
            {
                _sessionFactory = sessionFactory;
            }

            public List<StudentDto> Handle(GetListQuery command)
            {
                var unitOfWork = new UnitOfWork(_sessionFactory);

                var studentRepository = new StudentRepository(unitOfWork);

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
    }
}
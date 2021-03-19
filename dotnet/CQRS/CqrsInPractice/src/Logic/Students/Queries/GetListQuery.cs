using System.Collections.Generic;
using Logic.Dtos;

namespace Logic.Students.Queries
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
    }
}

using Logic.Dtos;

namespace Logic.Students.Commands
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
    }
}

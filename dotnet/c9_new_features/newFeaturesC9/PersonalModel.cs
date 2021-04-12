using System;
namespace newFeaturesC9
{
    public class PersonalModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public PersonalModel()
        {
        }

        public PersonalModel(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
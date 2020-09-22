using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkRelationshipsTesting.Entities
{
    public class Dog
    {
        public int DogId { get; set; }

        public string Name { get; set; }

        public AdditionalName AdditionalName { get; set; }

        public ICollection<Puppy> Puppies { get; set; }
    }

    public class AdditionalName
    {
        public int AdditionalNameId { get; set; }

        public string Name { get; set; }

        public int DogId { get; set; }
    }

    public class Puppy
    {
        public int PuppyId { get; set; }

        public string Name { get; set; }

        public Dog Dog { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkRelationshipsTesting.Entities
{
    public class Dog
    {
        public int DogId { get; set; }

        [Required]
        public string Name { get; set; }

        public DogBreeder DogBreeder { get; set; }

        public ICollection<DogOwnerDog> DogOwnerDogs { get; set; }

        public ICollection<Puppy> Puppies { get; set; }
    }

    public class DogOwnerDog
    {
        public Dog Dog { get; set; }

        public int DogId { get; set; }

        public DogOwner DogOwner { get; set; }

        public int DogOwnerId { get; set; }
    }

    public class DogOwner
    {
        public int DogOwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<DogOwnerDog> DogOwnerDogs { get; set; }
    }

    public class DogBreeder
    {
        public int DogBreederId { get; set; }

        [Required]
        public string Name { get; set; }

        public int DogId { get; set; }
    }

    public class Puppy
    {
        public int PuppyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public Dog Dog { get; set; }
    }
}

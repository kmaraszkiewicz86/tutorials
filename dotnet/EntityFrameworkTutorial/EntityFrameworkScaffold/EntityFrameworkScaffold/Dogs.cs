using System;
using System.Collections.Generic;

namespace EntityFrameworkScaffold
{
    public partial class Dogs
    {
        public Dogs()
        {
            DogOwnerDogs = new HashSet<DogOwnerDogs>();
            Puppies = new HashSet<Puppies>();
        }

        public int DogId { get; set; }
        public string Name { get; set; }

        public virtual DogBreeders DogBreeders { get; set; }
        public virtual ICollection<DogOwnerDogs> DogOwnerDogs { get; set; }
        public virtual ICollection<Puppies> Puppies { get; set; }
    }
}

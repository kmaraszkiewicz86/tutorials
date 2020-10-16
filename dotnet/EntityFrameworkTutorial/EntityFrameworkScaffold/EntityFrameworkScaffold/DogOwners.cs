using System;
using System.Collections.Generic;

namespace EntityFrameworkScaffold
{
    public partial class DogOwners
    {
        public DogOwners()
        {
            DogOwnerDogs = new HashSet<DogOwnerDogs>();
        }

        public int DogOwnerId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<DogOwnerDogs> DogOwnerDogs { get; set; }
    }
}

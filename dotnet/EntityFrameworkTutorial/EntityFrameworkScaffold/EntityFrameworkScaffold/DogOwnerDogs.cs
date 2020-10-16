using System;
using System.Collections.Generic;

namespace EntityFrameworkScaffold
{
    public partial class DogOwnerDogs
    {
        public int DogOwnerDogId { get; set; }
        public int DogId { get; set; }
        public int DogOwnerId { get; set; }

        public virtual Dogs Dog { get; set; }
        public virtual DogOwners DogOwner { get; set; }
    }
}

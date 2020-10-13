using System;
using System.Collections.Generic;

namespace EntityFrameworkScaffold
{
    public partial class DogBreeders
    {
        public int DogBreederId { get; set; }
        public string Name { get; set; }
        public int DogId { get; set; }

        public virtual Dogs Dog { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace EntityFrameworkScaffold
{
    public partial class Puppies
    {
        public int PuppyId { get; set; }
        public string Name { get; set; }
        public int? DogId { get; set; }

        public virtual Dogs Dog { get; set; }
    }
}

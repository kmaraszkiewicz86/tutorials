using System.Collections.Generic;

namespace AlghorithmsExampleProject.Helpers
{
    public interface IArrayHelper
    {
        (int numberToSearch, List<int> array) GenerateRandomLengthOfArray(int length, bool shouldSortAray, bool shouldRandomizeValueForSearch);
    }
}

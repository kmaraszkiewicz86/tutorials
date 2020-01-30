using System.Collections.Generic;
using AlghorithmsExampleProject.Extensions;

namespace AlghorithmsExampleProject.AlghoritmsTypes
{
    internal static class SearchType
    {
        public static string LinearSearch(int length)
        {
            var result = length.GenerateRandomLengthOfArray(true, true);
            var list = result.array;
            var numberToSearch = result.numberToSearch;

            var listOfIndexes = new List<int>();

            for (var index = 0; index < list.Count; index++)
            {
                if (list[index] == numberToSearch)
                {
                    listOfIndexes.Add(index + 1);
                }
            }

            return $"Liczba o wartosci {numberToSearch}, znajduje sie pod indeksami {string.Join(',', listOfIndexes)}";
        }

        public static int BinarySearch(int length)
        {
            var data = length.GenerateRandomLengthOfArray(true, true);

            var result = BinarySearch(data.array.ToArray(), data.numberToSearch, 0, data.array.Count - 1);
            return result > -1 ? result + 1 : result;
        }

        private static int BinarySearch(int[] array, int valueForSearch, int leftIndex, int rigthIndex)
        {
            if (leftIndex > rigthIndex)
                return -1;

            var mid = (leftIndex + rigthIndex) / 2;
            if (array[mid] == valueForSearch)
            {
                return mid;
            }

            if (array[mid] > valueForSearch)
            {
                return BinarySearch(array, valueForSearch, leftIndex, mid - 1);
            }

            return BinarySearch(array, valueForSearch, mid + 1, rigthIndex);

        }
    }
}
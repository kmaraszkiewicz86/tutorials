using System.Collections.Generic;
using AlghorithmsExampleProject.Helpers;

namespace AlghorithmsExampleProject.AlghoritmsTypes
{
    internal static class SearchType
    {
        public static IArrayHelper ArrayHelper { get; set; }

        public static string LinearSearch(int length)
        {
            var result = ArrayHelper.GenerateRandomLengthOfArray(length ,true, true);
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
            var data = ArrayHelper.GenerateRandomLengthOfArray(length, true, true);

            var result = BinarySearchRecursion(data.array.ToArray(), data.numberToSearch, 0, data.array.Count - 1);
            return result > -1 ? result + 1 : result;
        }

        private static int BinarySearchRecursion(int[] array, int valueForSearch, int leftIndex, int rigthIndex)
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
                return BinarySearchRecursion(array, valueForSearch, leftIndex, mid - 1);
            }

            return BinarySearchRecursion(array, valueForSearch, mid + 1, rigthIndex);

        }
    }
}
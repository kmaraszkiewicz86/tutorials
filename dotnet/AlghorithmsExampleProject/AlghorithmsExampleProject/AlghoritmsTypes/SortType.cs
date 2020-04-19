using System.Collections.Generic;
using AlghorithmsExampleProject.Helpers;

namespace AlghorithmsExampleProject.AlghoritmsTypes
{
    public static class SortType
    {
        public static IArrayHelper ArrayHelper { get; set; }

        public static int[] InsertionSort (int length)
        {
            var array = ArrayHelper.GenerateRandomLengthOfArray(length, false, false).array;

            for (var index = 0; index < array.Count; index++)
            {
                var minValIndex = index;

                for (var i = index; i < array.Count; i++)
                {
                    if (array[i] < array[minValIndex])
                        minValIndex = i;
                }

                var tmpValue = array[index]; 
                array[index] = array[minValIndex];
                array[minValIndex] = tmpValue;

            }

            return array.ToArray();
        }

        public static int[] BubbleSort(int length)
        {
            var array = ArrayHelper.GenerateRandomLengthOfArray(length, false, false).array;
            var shouldContinue = true;

            for (var index = 1; index < array.Count && shouldContinue; index++)
            {
                shouldContinue = false;
                for (var i = 0; i < array.Count - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        var tmp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = tmp;
                        shouldContinue = true;
                    }
                }
            }

            return array.ToArray();
        }

        public static int[] QuickSort(int length)
        {
            var array = ArrayHelper.GenerateRandomLengthOfArray(length, false, false).array;

            QuickSort(array, 0, array.Count - 1);

            return array.ToArray();
        }

        private static void QuickSort(List<int> array, int left, int right)
        {
            var leftTmp = left;
            var rightTmp = right;
            var pivot = array[(left + right) / 2];

            while (leftTmp < rightTmp)
            {
                while (pivot > array[leftTmp]) leftTmp++;
                while (pivot < array[rightTmp]) rightTmp--;

                if (leftTmp <= rightTmp)
                {
                    var tmp = array[leftTmp];
                    array[leftTmp] = array[rightTmp];
                    array[rightTmp] = tmp;
                    leftTmp++;
                    rightTmp--;
                }
            }

            if (left < rightTmp) QuickSort(array, left, rightTmp);
            if (right > leftTmp) QuickSort(array, leftTmp, right);
        }
    }
}
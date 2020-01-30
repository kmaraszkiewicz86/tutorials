using System.Collections.Generic;

namespace AlghorithmsExampleProject.AlghoritmsTypes
{
    internal static class OtherType
    {
        /// <summary>
        /// Najmniejszy Wspolny Dzielnik
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int SmallestSharedDivider(int a, int b)
        {
            if (b == 0)
                return a;

            return SmallestSharedDivider(b, a % b);
        }

        public static List<long> FibonacciAlorithmWithForLoop(int length)
        {
            if (length == 0)
                return new List<long>();

            if (length == 1)
                return new List<long>(new long[] { 1 });

            var list = new List<long>(new long[] { 1, 1 });

            for (int index = 2; index < length; index++)
            {
                list.Add(list[index - 2] + list[index - 1]);
            }

            return list;
        }

        public static List<long> FibonacciAlorithmWithRecursion(int arrayLength, List<long> array = null)
        {
            if (arrayLength < 0)
                return array;

            if (array == null)
            {
                if (arrayLength == 0)
                    return new List<long>();

                if (arrayLength == 1)
                    return new List<long>(new long[] { 1 });

                array = new List<long>(new long[] { 1, 1 });
                arrayLength -= 2;
            }
            else
            {
                array.Add(array[array.Count - 2] + array[array.Count - 1]);
            }

            return FibonacciAlorithmWithRecursion(arrayLength - 1, array);
        }
    }
}
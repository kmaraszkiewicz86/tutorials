using System.Collections.Generic;

namespace AlghorithmsExampleProject.AlghoritmsTypes
{
    internal static class OtherType
    {
        /// <summary>
        /// Najmniejszy Wspolny Dzielnik
        /// <code>
        ///    100 % 75
        ///     1. a = 100 b = 75
        ///     2. a = 75 b = 25
        ///     3. a = 25 b = 0
        ///     4. returns 25
        /// </code>
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

        public static int FibonacciAlorithm(int length)
        {
            var n1 = 0;
            var n2 = 1;
            var result = 0;

            for (int index = 0; index < length; index++)
            {
                result = n1 + n2;
                n1 = n2;
                n2 = result;
            }

            return result;
        }

        public static int FibonacciAlorithmRecursion(int length)
        {
            if (length <= 2)
                return length;


            return FibonacciAlorithmRecursion(length - 2)
                    + FibonacciAlorithmRecursion(length - 1);
        }

        public static List<long> FibonacciAlorithmWithForLoopReturnArray(int length)
        {
            if (length == 0)
                return new List<long>();

            if (length == 1)
                return new List<long>(new long[] {0, 1});

            var list = new List<long>(new long[] {0, 1, 1});

            for (int index = 2; index <= length; index++)
            {
                list.Add(list[index - 1] + list[index]);
            }

            return list;
        }

        public static List<long> FibonacciAlorithmWithRecursionReturnArray(int arrayLength)
        {
            if (arrayLength == 0)
                return new List<long>();

            if (arrayLength == 1)
                return new List<long>(new long[] {0, 1});

            var array = new List<long>(new long[] {0, 1, 1});
            arrayLength -= 1;

            return FibonacciAlorithmWithRecursionReturnArray(arrayLength, array);
        }

        private static List<long> FibonacciAlorithmWithRecursionReturnArray(int arrayLength, List<long> array = null)
        {
            if (arrayLength <= 0)
                return array;


            array.Add(array[array.Count - 2] + array[array.Count - 1]);


            return FibonacciAlorithmWithRecursionReturnArray(arrayLength - 1, array);
        }

    }
}
using System;
using System.Collections.Generic;
using AlghorithmsExampleProject.AlghoritmsTypes.TreeImpl;

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

        public static int SmallestSharedDividerForLoop(int a, int b)
        {
            var result = b;

            do
            {
                b = result;
                result = a % result;
                a = b;

            } while (result > 0);

            return b;
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
            if (length < 2)
                return length;


            return FibonacciAlorithmRecursion(length - 2)
                    + FibonacciAlorithmRecursion(length - 1);
        }

        public static List<long> FibonacciAlorithmWithForLoopReturnArray(int length)
        {
            if (length == 0)
                return new List<long>();

            if (length == 1)
                return new List<long>(new long[] { 0, 1 });

            var list = new List<long>(new long[] { 0, 1, 1 });

            for (int index = 2; index < length; index++)
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
                return new List<long>(new long[] { 0, 1 });

            var array = new List<long>(new long[] { 0, 1, 1 });
            arrayLength -= 1;

            return FibonacciAlorithmWithRecursionReturnArray(arrayLength, array);
        }

        public static void CheckIfTextHasValidPairsOfBrackets()
        {
            Console.WriteLine("CheckIfTextHasValidPairsOfBrackets");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();

            CheckIfTextHasValidPairsOfBrackets("((()))()())))");
            CheckIfTextHasValidPairsOfBrackets("()");
            CheckIfTextHasValidPairsOfBrackets("(((())))");
            CheckIfTextHasValidPairsOfBrackets("()()()((())");
            CheckIfTextHasValidPairsOfBrackets("()(())()(");

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
        }

        public static void CheckIfTextHasValidPairsOfBracketsRecursion()
        {
            Console.WriteLine("CheckIfTextHasValidPairsOfBracketsRecursion");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();

            var arr = new string[]
            {
                "((()))()())))",
                "()",
                "(((())))",
                "()()()((())",
                "()(())()("
            };

            foreach (var item in arr)
            {
                var total = 0;
                CheckIfTextHasValidPairsOfBracketsRecursion(item, item.Length - 1, ref total);
                Console.WriteLine($"In the text {item} has {total == 0} pairs of bracket characters");
            }

            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
        }

        public static void CheckIfTextHasValidPairsOfBracketsRecursion(string text, int index, ref int total)
        {
            if (index < 0)
                return;

            switch (text[index])
            {
                case '(':
                    total++;
                    break;

                case ')':
                    total--;
                    break;

                default:
                    break;
            }

            CheckIfTextHasValidPairsOfBracketsRecursion(text, index - 1, ref total);
        }

        public static void CheckIfTextHasValidPairsOfBrackets(string text)
        {
            var total = 0;
            foreach (char character in text)
            {
                switch (character)
                {
                    case '(':
                        total++;
                        break;

                    case ')':
                        total--;
                        break;

                    default:
                        break;
                }
            }

            Console.WriteLine($"In the text {text} has {total == 0} pairs of bracket characters");
        }

        public static HashSet<int> HastableTest()
        {
            var hastable = new HashSet<int>();
            hastable.Add(2);
            hastable.Add(2);
            hastable.Add(2);
            hastable.Add(2);
            hastable.Add(2);
            hastable.Add(1);
            hastable.Add(1);
            hastable.Add(1);
            hastable.Add(1);

            return hastable;
        }

        public static Stack<char> StackTest(string testString)
        {
            var stack = new Stack<char>();

            var invertedText = InvertText(testString);

            if (string.IsNullOrWhiteSpace(invertedText))
                return stack;

            Console.WriteLine($"Adding testing characters into stack from inverted string => {invertedText}");

            foreach (var character in invertedText)
            {
                stack.Push(character);
            }

            return stack;
        }

        public static Queue<char> QueueTest(string testString)
        {
            var queue = new Queue<char>();

            var invertedText = InvertText(testString);

            if (string.IsNullOrWhiteSpace(invertedText))
                return queue;

            Console.WriteLine($"Adding testing characters into Queue from inverted string => {invertedText}");

            foreach (var character in invertedText)
            {
                queue.Enqueue(character);
            }

            return queue;
        }

        private static string InvertText(string testString)
        {
            if (string.IsNullOrWhiteSpace(testString))
            {
                Console.WriteLine($"testString is required");
                return string.Empty;
            }

            var invertedText = "";

            for (var index = testString.Length - 1; index >= 0; index--)
            {
                invertedText += testString[index];
            }

            return invertedText;
        }

        public static void TreeNodeTes()
        {
            Console.WriteLine("---------------------Tree impl-------------------------------");


            var tree = new Tree<int>
            {
                Root = new TreeNode<int>
                {
                    Data = 10
                }
            };

            tree.Root.AddChildren(new List<TreeNode<int>>(new[] {
                new TreeNode<int>
                {
                    Data = 23
                },
                new TreeNode<int>
                {
                    Data = 230
                },
                new TreeNode<int>
                {
                    Data = 11
                },
                new TreeNode<int>
                {
                    Data = 2
                }
            }));

            tree.Root.Children[2].AddChildren(new[]
            {
                new TreeNode<int>
                {
                    Data = 123
                },
                new TreeNode<int>
                {
                    Data = 345
                }
            });

            Console.WriteLine("---------------------Tree impl-------------------------------");
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
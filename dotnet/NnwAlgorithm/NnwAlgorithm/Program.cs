using System;
using NnwAlgorithm.Model;

namespace NnwAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    Console.WriteLine("App calculates greathest common divider");
                    Console.WriteLine("=======================================");

                    var number1 = PutNumber("first");

                    var number2 = PutNumber("second");

                    Nwd.CheckIfNumber1IsGreatherThenSecond(number1, number2);

                    Console.WriteLine(Nwd.Calculation(number1, number2));
                    Console.WriteLine("================================================");
                }
                catch (Exception err)
                {
                    Console.WriteLine("While app tring to comapre number, occours error");
                    Console.WriteLine(err.Message);

                }
                Console.WriteLine("=====================================================");
                Console.WriteLine("Press enter to leave or other key to continue:");
            }
            while (Console.ReadKey().Key != ConsoleKey.Enter);
        }

        /// <summary>
        /// Put number in console
        /// </summary>
        /// <param name="numberOfInteger">The number of typed number</param>
        /// <returns>The number converted to integer</returns>
        static int PutNumber(string numberOfInteger)
        {
            Console.Write($"Set {numberOfInteger} number: ");
            string result = Console.ReadLine();
            int number1 = Nwd.ValidateNumber(result);
            Console.WriteLine();
            return number1;
        }
    }
}
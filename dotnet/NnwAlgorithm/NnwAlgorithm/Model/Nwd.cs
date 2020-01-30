using System;

namespace NnwAlgorithm.Model
{
    /// <summary>
    /// The Nwd class
    /// </summary>
    class Nwd
    {
        /// <summary>
        /// Validates if number is integer type
        /// </summary>
        /// <param name="number">The number in string that will be converted to integer</param>
        /// <returns>Converted integer</returns>
        public static int ValidateNumber(string number)
        {
            if (int.TryParse(number, out var num))
            {
                return num;
            }

            throw new Exception("The number isnt valid integer. Please try type numbers again.");
        }

        /// <summary>
        /// Check if first number is greather then second
        /// </summary>
        /// <param name="number1">The first number to compare</param>
        /// <param name="number2">The second number to compare</param>
        public static void CheckIfNumber1IsGreatherThenSecond(int number1, int number2)
        {
            if (number1 <= number2) throw new Exception("First number is lower or equal then second number. Please try again.");
        }

        /// <summary>
        /// Calculate nwd
        /// </summary>
        /// <param name="number1">The first number</param>
        /// <param name="number2">The second number</param>
        /// <returns>The grathes common  divisior</returns>
        public static int Calculation(int number1, int number2)
        {
            if (number2 == 0)
                return number1;

            return Calculation(number2, number1 % number2);
        }
    }
}
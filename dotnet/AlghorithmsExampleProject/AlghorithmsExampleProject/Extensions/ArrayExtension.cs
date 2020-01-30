using System;
using System.Collections.Generic;

namespace AlghorithmsExampleProject.Extensions
{
    public static class ArrayExtension
    {
        public static (int numberToSearch, List<int> array) GenerateRandomLengthOfArray(this int length, bool shouldSortAray, bool shouldRandomizeValueForSearch)
        {
            if (length < 1)
                return (-1, new List<int>());

            var array = new List<int>();


            var random = new Random();

            Console.WriteLine($"Generuje tablice o wielkości {length}");

            for (int index = 0; index < length; index++)
            {
                array.Add(random.Next(-1000, 1000));
            }

            if (shouldSortAray)
            {
                array.Sort((a, b) =>
                {
                    if (a > b)
                        return 1;

                    if (a < b)
                        return -1;

                    return 0;
                });
            }

            var listOfArrayToShow = new List<string>();

            for (var index = 0; index < array.Count; index++)
            {
                listOfArrayToShow.Add($"{index + 1} => {array[index]}");
            }

            Console.WriteLine($"Wygenerowalem tablice z elementami =>");
            Console.WriteLine(string.Join("; ", listOfArrayToShow));
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("---------------------------------------------------------------------------");

            var numberToSearch = -1;

            if (shouldRandomizeValueForSearch)
            {
                Console.WriteLine("Losuje liczne do wyszukania...");

                numberToSearch = array[new Random().Next(0, array.Count)];

                Console.WriteLine($"Liczba do wyszukiwania to liczba: {numberToSearch}");
            }

            return (numberToSearch, array);
        }
    }
}

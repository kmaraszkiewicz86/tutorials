using System;
using ProgrammingPatterns.Core;

namespace ProgrammingPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Builder implemnetation");
            //BuilderExample.DoWork();
            AbstractFactoryExample.DoWork();
            
            Console.ReadKey();
        }
    }
}
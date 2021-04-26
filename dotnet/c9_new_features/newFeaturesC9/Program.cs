using System;
using newFeaturesC9;

Console.WriteLine("Hello World!");
Console.WriteLine(Add(4,3));

PersonalModel p = new() { Id = 1, FirstName = "Krzysztof", LastName = "M" };

PersonalModel p2 = new(2, "Anna", "G");

PersonalModel p3 = null;

if (p3 is null)
{
    Console.WriteLine("p3 is null");
}

if (p2 is not null)
{
    Console.WriteLine("p2 is not null");
}

static double Add(double x, double y)
{
    return x + y;
}
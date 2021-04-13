using System;

namespace RecordDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var record1 = new Record1("Lilo", "Gruberszt");
            var record2 = new Record1("Izka", "Marliska");
            var record3 = new Record1("Szarlotka", "Krowiejka");
            var record4 = new Record1("Mailo", "Pikorz");

            var rec = new Record1("Lilo", "Gruberszt");

            Equals(rec, record1);
            Equals(rec, record2);
            Equals(rec, record3);
            Equals(rec, record4);

            RunDeconstructForRecord(rec);

            CreateDeepCopyOfRecord(rec);

            RecordWithOverridenProperty();

            RecordAfterInheritanceExample(rec);

            var class1 = new Class1("Lilo", "Gruberszt");
            var class2 = new Class1("Izka", "Marliska");
            var class3 = new Class1("Szarlotka", "Krowiejka");
            var class4 = new Class1("Mailo", "Pikorz");

            var c = new Class1("Lilo", "Gruberszt");

            Equals(c, class1);
            Equals(c, class2);
            Equals(c, class3);
            Equals(c, class4);

            class1.Deconstruct(out var fn2, out var ln2);
            Console.WriteLine($"Deconstructor values => {fn2}, {ln2}");
        }

        private static void RecordAfterInheritanceExample(Record1 rec)
        {
            Console.WriteLine("RecordAfterInheritanceExample: ");

            var recordAfterInheritance = new RecordAfterInheritance(1, rec.FirstName, rec.LastName);
            Console.WriteLine($"RecordAfterInheritance: {rec}");

            Console.WriteLine($"{recordAfterInheritance} == {rec} => {recordAfterInheritance == rec}");
        }

        private static void RecordWithOverridenProperty()
        {
            Record2 record21 = new Record2("Lilo", "Mailo");

            Console.WriteLine($"Record2: {record21}");

            Console.WriteLine(record21.SayHallo());
        }

        private static void CreateDeepCopyOfRecord(Record1 rec)
        {
            Record1 copyOfRecord = rec with
            {
                FirstName = "Tom"
            };

            Equals(rec, copyOfRecord);
        }

        private static void RunDeconstructForRecord(Record1 rec)
        {
            var (fn, ln) = rec;
            Console.WriteLine($"Deconstructor values => {fn}, {ln}");
        }

        static void Equals(Record1 a, Record1 b)
        {
            Console.WriteLine($"{a} == {b} => {a == b}");
        }

        static void Equals(Class1 a, Class1 b)
        {
            Console.WriteLine($"{a} == {b} => {a == b}");
        }
    }

    public record Record1(string FirstName, string LastName);

    public record Record2(string FirstName, string LastName)
    {
        private string FirstName { get; init; } = "test";

        public string FullName => $"{FirstName} {LastName}";

        public string SayHallo()
        {
            return $"Hello { FirstName }";
        }
    }

    public record RecordAfterInheritance(int Id, string FirstName, string LastName) : Record1(FirstName, LastName);

    public class Class1
    {
        public string Firstname { get; init; }

        public string Lastname { get; init; }

        public Class1(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public static bool operator ==(Class1 a, Class1 b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Class1 a, Class1 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return obj is Class1 @class &&
                   Firstname == @class.Firstname &&
                   Lastname == @class.Lastname;
        }

        public override string ToString()
        {
            return "{" + nameof(Firstname) + "= " + Firstname + ", " + nameof(Lastname) + "= " + Lastname + " }";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Firstname, Lastname);
        }

        public void Deconstruct(out string firstname, out string lastname)
        {
            firstname = Firstname;
            lastname = Lastname;
        }
    }

}

namespace GetHashCodeExample
{
    internal enum Test
    {
        None = 0,
        All,
        Single
    }
    
    internal class TestClass
    {
        
        public int Id { get; set; }

        public string Name { get; set; }

        public Test Test { get; set; }

        public TestClass(int id, string name, Test test)
        {
            Id = id;
            Name = name;
            Test = test;
        }

        public static bool operator ==(TestClass obj1, TestClass obj2)
        {
            return obj1.Id != null 
                   && obj2.Id != null 
                   && obj1.Id == obj2.Id
                   && obj1.Name == obj2.Name 
                   && obj1.Test == obj2.Test;
        }

        public static bool operator !=(TestClass obj1, TestClass obj2)
        {
            return !(obj1 == obj2);
        }

        public override bool Equals(object? obj)
        {
            var testClass = obj as TestClass;
            if (testClass != null)
            {
                return Id == testClass.Id 
                       && Name == testClass.Name 
                       && Test == testClass.Test;
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Name.GetHashCode() ^ Test.GetHashCode();;
        }
    }
}
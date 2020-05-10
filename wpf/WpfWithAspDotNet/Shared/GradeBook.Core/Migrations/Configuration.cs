using System.Collections.Generic;
using GradeBook.Core.Entities;

namespace GradeBook.Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<GradeBook.Core.Core.GradeBookDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GradeBook.Core.Core.GradeBookDbContext context)
        {
            context.Students.AddRange(new List<Student>
            {
                new Student
                {
                    Id = 1,
                    Name = "Karol",
                    Lastname = "Wickowski"
                },
                new Student
                {
                    Id = 1,
                    Name = "Marcin",
                    Lastname = "Krzystofowicz"
                },
                new Student
                {
                    Id = 1,
                    Name = "Jarosław",
                    Lastname = "Górny"
                },
                new Student
                {
                    Id = 1,
                    Name = "Marek",
                    Lastname = "Wojciechowski"
                },
                new Student
                {
                    Id = 1,
                    Name = "Jan",
                    Lastname = "Majka"
                },
                new Student
                {
                    Id = 1,
                    Name = "Krzysztof",
                    Lastname = "Wrycek"
                }
            });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

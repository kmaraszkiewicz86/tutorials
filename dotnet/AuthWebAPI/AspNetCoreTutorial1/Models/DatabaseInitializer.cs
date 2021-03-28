using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTutorial1.Models.EntityFramework;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreTutorial1.Models
{
    public static class DatabaseInitializer
    {
	    public static void Seed(MigrationBuilder migrationBuilder)
	    {
			
		    migrationBuilder.InsertData("CarTypeModels", new[] {"Id", "Name"}, new object[] {1, "BMW"}, string.Empty);
		    migrationBuilder.InsertData("CarModels", new[] {"Id", "Name", "TypeId" }, new object[] {1, "BMW", 1}, string.Empty);
		    migrationBuilder.InsertData("DriverModels", new[] {"Id", "Name" }, new object[] {1, "3" }, string.Empty);
		    migrationBuilder.InsertData("DriverModelCarModels", new[] {"Id", "CarModelId", "DriverModelId" }, new object[] {1, 1, 1}, string.Empty);
			
			//context.Database.EnsureCreated();

			//var type = new CarTypeModel
			//{
			// Name = "BMW"
			//};

			//var car = new CarModel
			//{
			// Name = "3",
			// Type = type,
			//};

			//var driver = new DriverModel
			//{
			// Name = "Bobik Testowy",
			// Cars = new List<CarModel> {car}
			//};

			//context.CarTypeModels.Add(type);
			//context.CarModels.Add(car);
			//context.DriverModels.Add(driver);

			//context.SaveChanges();
		}
    }
}

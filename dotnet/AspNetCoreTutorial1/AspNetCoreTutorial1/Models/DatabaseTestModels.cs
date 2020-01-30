using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTutorial1.Models
{
	public class LogEntry
	{
		public int Id { get; set; }

		[Required] public DateTime TimeStamp { get; set; }

		[Required] public string Message { get; set; }

		[Required] public string Level { get; set; }

		[Required] public string Logger { get; set; }
	}

	public class CarModel
    {
	    public int Id { get; set; }

		[Required]
	    public string Name { get; set; }

	    [Required]
		public CarTypeModel Type { get; set; }

	    public ICollection<DriverModelCarModel> DriverModels { get; set; }
    }

	public class DriverModel
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		public ICollection<DriverModelCarModel> Cars;
	}

	public class DriverModelCarModel
	{
		public int Id { get; set; }

		[Required]
		public DriverModel DriverModel { get; set; }

		[Required]
		public CarModel CarModel { get; set; }
	}
	
	public class CarTypeModel
	{
		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }
	}
}

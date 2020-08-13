using BethanysPieShopHRM.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorAppTutorial.Services
{
    public interface IJobCategoryDataService
	{
		Task<IEnumerable<JobCategory>> GetAllJobCategories();
		Task<JobCategory> GetJobCategoryById(int jobCategoryId);
	}
}

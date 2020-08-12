using System.Collections.Generic;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using BlazorAppTutorial.Api.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTutorial.Pages
{
    public class EmployeeOverviewBase : ComponentBase
    {
        public IEnumerable<Employee> Employees { get; set; }

		[Inject]
		public IEmployeeDataService EmployeeDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
			Employees = await EmployeeDataService.GetAllEmployees();
        }
	}
}

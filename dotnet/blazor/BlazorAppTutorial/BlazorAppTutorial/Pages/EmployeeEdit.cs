using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using BlazorAppTutorial.Api.Services;
using BlazorAppTutorial.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTutorial.Pages
{
    public partial class EmployeeEdit
    {
        [Parameter]
        public string EmployeeId { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Inject]
        public ICountryDataService CountryDataService { get; set; }

        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }

        public Employee Employee { get; set; } = new Employee();

        public List<Country> Countries { get; set; } = new List<Country>();

        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        public string CountryId { get; set; } = string.Empty;

        public string JobCategoryId { get; set; } = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            //Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));

            int.TryParse(EmployeeId, out var emplyeeId);

            if (employeeId == 0) //new employee is being created
            {
                //add some defaults
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
            }
            else
            {
                Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            }

            Countries = (await CountryDataService.GetAllCountries()).ToList();
            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();
        }

        public async Task HandleValidSubmit()
        {
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            if (Employee.EmployeeId == 0)
            {
                var addedEmployee = EmployeeDataService.AddEmployee(Employee);
            }
            else
            {
                await EmployeeDataService.UpdateEmployee(Employee);
            }
        }
    }
}

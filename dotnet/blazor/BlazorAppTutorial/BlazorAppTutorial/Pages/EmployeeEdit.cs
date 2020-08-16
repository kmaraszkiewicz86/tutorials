using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using BlazorAppTutorial.Api.Services;
using BlazorAppTutorial.Services;
using Microsoft.AspNetCore.Components;
using System;

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

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Employee Employee { get; set; } = new Employee();

        public List<Country> Countries { get; set; } = new List<Country>();

        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        public string CountryId { get; set; } = string.Empty;

        public string JobCategoryId { get; set; } = string.Empty;

        protected string Message = string.Empty;

        protected string StatusClass = string.Empty;

        protected bool Saved;

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

            int.TryParse(EmployeeId, out var employeeId);

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
            Saved = false;

            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            if (Employee.EmployeeId == 0)
            {
                var addedEmployee = EmployeeDataService.AddEmployee(Employee);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfuly.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding new employee. Please try again later.";
                    Saved = false;
                }

            }
            else
            {
                try
                {
                    await EmployeeDataService.UpdateEmployee(Employee);

                    StatusClass = "alert-success";
                    Message = "Employee updated successfuly.";
                    Saved = true;
                }
                catch (Exception err)
                {
                    StatusClass = "alert-danger";
                    Message = err.Message;
                    Saved = true;
                }
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors.";
        }

        protected async Task DeleteEmplyee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfuly.";
            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }
    }
}

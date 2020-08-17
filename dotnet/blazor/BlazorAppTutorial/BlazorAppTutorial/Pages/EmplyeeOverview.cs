using System.Collections.Generic;
using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using BlazorAppTutorial.Api.Services;
using BlazorAppTutorial.Components;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTutorial.Pages
{
    public partial class EmplyeeOverview
    {
        public IEnumerable<Employee> Employees { get; set; }

        protected AddEmployeeDialog AddEmployeeDialog { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employees = await EmployeeDataService.GetAllEmployees();
        }

        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        protected async Task AddEmployeeDialog_OnCloseDialog()
        {
            Employees = await EmployeeDataService.GetAllEmployees();
            StateHasChanged();
        }

    }
}

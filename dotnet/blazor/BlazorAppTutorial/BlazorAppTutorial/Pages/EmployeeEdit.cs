using System.Threading.Tasks;
using BethanysPieShopHRM.Shared;
using BlazorAppTutorial.Api.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorAppTutorial.Pages
{
    public partial class EmployeeEdit
    {
        [Parameter]
        public string EmployeeId { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        public Employee Employee { get; set; } = new Employee();

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
        }
    }
}

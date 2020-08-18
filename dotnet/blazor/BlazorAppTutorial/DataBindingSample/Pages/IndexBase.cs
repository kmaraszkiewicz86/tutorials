using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataBindingSample.Pages
{
    public class IndexBase : ComponentBase
    {
        public Employee Employee { get; set; }

        protected override Task OnInitializedAsync()
        {
            Employee = new Employee
            {
                FirstName = "Grzegorz",
                LastName = "ZDupy"
            };

            return base.OnInitializedAsync();
        }

        public void Click()
        {
            Employee.FirstName = "Orientek";
        }
    }
}

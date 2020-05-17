using System.Collections.Generic;
using PersonalInfomationWebService.Models.Responses;

namespace PersonalInfomationWebService.Services.Interfaces
{
    public interface IPeopleService
    {
        PeopleCollectionModelResponse GetAll();
    }
}

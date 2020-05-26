using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalInfomationWebService.Models.Requests;
using PersonalInfomationWebService.Models.Responses;

namespace PersonalInfomationWebService.Services.Interfaces
{
    public interface IPeopleService
    {
        PeopleCollectionModelResponse GetAll();

        Task<PersonModelResponse> Get(PersonModelRequest model);
    }
}

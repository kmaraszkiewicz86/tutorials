using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using PersonalInfomationWebService.Models.Requests;
using PersonalInfomationWebService.Models.Responses;

namespace PersonalInfomationWebService.ServiceContracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPersonalInforamtionService" in both code and config file together.
    [ServiceContract]
    public interface IPersonalInforamtionService
    {
        [OperationContract]
        PeopleCollectionModelResponse GetAll();

        [OperationContract]
        Task<PersonModelResponse> GetAsync(PersonModelRequest model);
    }
}

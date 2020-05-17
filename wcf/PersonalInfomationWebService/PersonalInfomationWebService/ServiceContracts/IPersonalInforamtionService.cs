using System.Collections.Generic;
using System.ServiceModel;
using PersonalInfomationWebService.Models.Responses;

namespace PersonalInfomationWebService.ServiceContracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IPersonalInforamtionService" in both code and config file together.
    [ServiceContract]
    public interface IPersonalInforamtionService
    {
        [OperationContract]
        PeopleCollectionModelResponse GetAll();
    }
}

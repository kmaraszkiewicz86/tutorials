using System.Threading.Tasks;
using PersonalInfomationWebService.Models.Requests;
using PersonalInfomationWebService.Models.Responses;
using PersonalInfomationWebService.ServiceContracts;
using PersonalInfomationWebService.Services.Interfaces;

namespace PersonalInfomationWebService.ServiceImplementations
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PersonalInforamtionService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PersonalInforamtionService.svc or PersonalInforamtionService.svc.cs at the Solution Explorer and start debugging.
    public class PersonalInforamtionService : IPersonalInforamtionService
    {
        private IPeopleService _peopleService;

        public PersonalInforamtionService(IPeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        public PeopleCollectionModelResponse GetAll()
        {
            return _peopleService.GetAll();
        }

        public async Task<PersonModelResponse> GetAsync(PersonModelRequest model)
        {
            return await _peopleService.Get(model);
        }
    }
}

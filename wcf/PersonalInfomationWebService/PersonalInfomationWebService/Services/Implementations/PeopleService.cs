using System.Collections.Generic;
using PersonalInfomationWebService.Models.Responses;
using PersonalInfomationWebService.Services.Interfaces;

namespace PersonalInfomationWebService.Services.Implementations
{
    public class PeopleService: IPeopleService
    {
        public PeopleCollectionModelResponse GetAll()
        {
            return new PeopleCollectionModelResponse(new List<PersonModelResponse>
            {
                new PersonModelResponse(1, "Jan", "Kowalski")
            });
        }
    }
}
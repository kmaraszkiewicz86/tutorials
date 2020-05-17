using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PersonalInfomationWebService.Models.Responses
{
    [DataContract]
    public class PeopleCollectionModelResponse
    {
        [DataMember]
        public IEnumerable<PersonModelResponse> People { get; set; }

        public PeopleCollectionModelResponse(IEnumerable<PersonModelResponse> people)
        {
            People = people;
        }
    }
}
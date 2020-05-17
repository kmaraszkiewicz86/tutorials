using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PersonalInfomationWebService.Models.Responses
{
    [DataContract]
    public class PersonModelResponse
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Surname { get; set; }

        public PersonModelResponse(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
    }
}
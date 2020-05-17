using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PersonalInfomationWebService.Models.Requests
{
    public class PersonModelRequest
    {
        [Required]
        [Range(1, 9999)]
        public int PersonId { get; set; }
    }
}
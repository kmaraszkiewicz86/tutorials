//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PersonalInfomationWebService.Core
{
    using System;
    using System.Collections.Generic;
    
    public partial class PEOPLE
    {
        public decimal ID { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public Nullable<decimal> GENDERID { get; set; }
    
        public virtual GENDERS GENDERS { get; set; }
    }
}

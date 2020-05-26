using System;

namespace PersonalInfomationWebService.Expetions
{
    public class NotFoundExceptions: Exception
    {
        public NotFoundExceptions(string message) : base(message)
        {
        }
    }
}
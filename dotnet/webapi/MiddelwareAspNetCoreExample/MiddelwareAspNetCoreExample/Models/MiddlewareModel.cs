using System.Collections.Generic;

namespace MiddelwareAspNetCoreExample.Models
{
    public class MiddlewareModel
    {
        public List<string> Messages { get; private set; }

        private int _stepNumber;

        public MiddlewareModel()
        {
            Messages = new List<string>();
        }

        public void AddNewMessageWithStepNumberIncrementations(string message)
        {
            Messages.Add($"{message}.{(++_stepNumber)}");
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NotifyDataErrorInfo.ViewModels
{
    public class ViewModel: INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public bool ErrorFree { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public bool HasErrors
        {
            get { return _errors.Any(x => x.Value != null && x.Value.Count > 0); }
        }

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        private string _name;

        [DisplayName("Name")]
        [Required]
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _nick;

        [DisplayName("Nick")]
        [Required]
        public string Nick
        {
            get => _nick;
            set
            {
                _nick = value;
                OnPropertyChanged("Nick");
            }
        }

        private string _email;

        [DisplayName("Email")]
        [Required]
        [StringLength(40, MinimumLength = 4)]
        [EmailAddress]
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        private string _age;

        [DisplayName("Age")]
        [Required]
        [CustomValidation(typeof(ViewModel), "AgeValidation")]
        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged("Age");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Validate()
        {
            ValidationContext validationContext = new ValidationContext(this, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, validationContext, validationResults, true);

            foreach (var er in _errors.ToList())
            {
                if (validationResults.All(r => r.MemberNames.All(m => m != er.Key)))
                {
                    _errors.Remove(er.Key);
                    OnErrorsChanged(er.Key);
                }
            }

            var members = from v in validationResults
                from m in v.MemberNames
                group v by m into o
                select o;

            foreach (var member in members)
            {
                var messages = member.Select(x => x.ErrorMessage).ToList();

                if (_errors.ContainsKey(member.Key))
                {
                    _errors.Remove(member.Key);
                }
                _errors.Add(member.Key, messages);
                OnErrorsChanged(member.Key);
            }

            ErrorFree = !HasErrors;

        }

        public void OnErrorsChanged(string propertyName_)
        {
            if (ErrorsChanged != null)
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName_));
        }

        public IEnumerable GetErrors(string propertyName_)
        {
            List<string> errors;
            _errors.TryGetValue(propertyName_, out errors);
            return errors;
        }

        public static ValidationResult AgeValidation(object obj, ValidationContext context)
        {
            var user = (ViewModel) context.ObjectInstance;

            int probe = 0;
            Int32.TryParse(user.Age, out probe);
            if (probe < 1 || probe > 150)
                return new ValidationResult("Ile?", new List<string> { "Age" });

            return ValidationResult.Success;
        }

        
        
    }
}

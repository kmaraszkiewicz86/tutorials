namespace CreditRatingClient.ViewModels
{
    public class ResultModel: ViewModelBase
    {
        private string _message;

        public string Message 
        { 
            get => _message; 
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }
    }
}

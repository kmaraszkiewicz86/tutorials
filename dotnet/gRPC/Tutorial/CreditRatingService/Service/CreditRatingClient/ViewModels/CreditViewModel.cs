using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using CreditRatingShared;
using Grpc.Net.Client;

namespace CreditRatingClient.ViewModels
{
    public class CreditViewModel : ViewModelBase
    {
        private ResultModel _resultModel;

        public ResultModel ResultModel
        {
            get => _resultModel;
            set
            {
                _resultModel = value;
                OnPropertyChanged(nameof(ResultModel));
            }
        }

        public void ChangeValue()
        {
            ResultModel = new ResultModel
            {
                Message = "Initializing data from server"
            };

            Task.Factory.StartNew(async () =>
                {
                    var request = new CreditRequest
                    {
                        CustomerId = "id0201",
                        Credit = 7000
                    };

                    var channel = GrpcChannel.ForAddress("https://localhost:5001/");
                    var client = new CreditRatingCheck.CreditRatingCheckClient(channel);
                    var response = await client.CheckCreditRequestAsync(request);

                    ResultModel.Message = $"Credit for customer {request.CustomerId} {(response.IsAcepted ? "approved" : "rejected")}!";
                });
        }
    }

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }
    }


}

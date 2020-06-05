using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace StackLayoutTutorial
{
    public partial class MySecondPage : ContentPage
    {
        public MySecondPage()
        {
            InitializeComponent();
        }
    }

    public class MySecondPageViewModel : INotifyPropertyChanged
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

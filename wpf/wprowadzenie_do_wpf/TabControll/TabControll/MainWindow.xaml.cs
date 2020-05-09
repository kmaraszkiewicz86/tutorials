using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace TabControll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private DispatcherTimer timer;

        private bool isSliderMoving = false;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new OpenFileDialog();
            openDialog.Filter = "MP3|*.mp3|All files|*.*";
            if (openDialog.ShowDialog() == true)
            {
                mediaPlayer.Open(new Uri(openDialog.FileName));
                audioTextBlock.Text = $"Audio: {openDialog.FileName}";
                playButton.IsEnabled = true;
                pauseButton.IsEnabled = true;
                stopButton.IsEnabled = true;
                timeSlider.IsEnabled = true;
                timer.Start();
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Play();
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var radio = sender as RadioButton;
            string color = (radio.Content.ToString() == "Blue") ?
                "LightSkyBlue" : "LightGreen";
            plaingProgressBar.Foreground = (SolidColorBrush) new
                BrushConverter().ConvertFromString(color);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null && mediaPlayer.NaturalDuration.HasTimeSpan
                && !isSliderMoving)
            {
                timeTextBlock.Text = mediaPlayer.Position.ToString(@"mm\:ss");

                TimeSpan ts = mediaPlayer.NaturalDuration.TimeSpan;
                plaingProgressBar.Maximum = 100;
                plaingProgressBar.Value =
                    ((double) mediaPlayer.Position.TotalMilliseconds / ts.TotalMilliseconds) * 100;
                timeSlider.Maximum = ts.TotalMilliseconds;
                timeSlider.Value = mediaPlayer.Position.TotalMilliseconds;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

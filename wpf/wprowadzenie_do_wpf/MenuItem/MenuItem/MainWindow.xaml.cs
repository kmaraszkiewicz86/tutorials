using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Microsoft.Win32;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "WebPage|*.html|Testowanie|*.jpg";
            dialog.DefaultExt = ".html";
            dynamic doc = WebBrowser.Document;
            if (doc != null)
            {
                var htmlText = doc.documentElement.InnerHtml;
                if (dialog.ShowDialog() == true && htmlText != null)
                {
                    File.WriteAllText(dialog.FileName, htmlText);
                }
            }
        }

        private void PrintMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FrameMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            if (frameBorder != null) frameBorder.BorderThickness = new Thickness(3);
        }

        private void FrameMenuItem_Unchecked(object sender, RoutedEventArgs e)
        {

            if (frameBorder != null) frameBorder.BorderThickness = new Thickness(0);
        }

        private void TmpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The options is under construction");
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Simple browser, Version 0.1");
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (WebBrowser.CanGoBack)
                WebBrowser.GoBack();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            if (WebBrowser.CanGoForward)
                WebBrowser.GoForward();
        }

        private void adressText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NavigateToPage();
            }
        }

        private void enterBuytton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage();
        }

        private void WebBrowser_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            adressText.Text = e.Uri.OriginalString;
        }

        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void HideScriptErrors(WebBrowser webBrowser, bool hide)
        {
            dynamic activeX = webBrowser.GetType()
                .InvokeMember("ActiveXInstance",
                    BindingFlags.GetProperty | BindingFlags.Instance |
                    BindingFlags.NonPublic, null, webBrowser, new object[] { });
            activeX.Silent = true;
        }

        private void NavigateToPage()
        {
            WebBrowser.Navigate(adressText.Text);
        }

        private void TreeViewTestExample_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

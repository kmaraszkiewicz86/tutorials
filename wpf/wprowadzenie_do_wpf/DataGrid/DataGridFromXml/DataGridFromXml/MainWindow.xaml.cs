using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _xmlFilePath = "Produkty.xml";

        private XElement _productsFromXml;

        public MainWindow()
        {
            InitializeComponent();
            CreateDataContext();
        }

        private void CreateDataContext()
        {
            if (File.Exists(_xmlFilePath))
            {
                _productsFromXml = XElement.Load(_xmlFilePath);
            }

            xmlDataGrid.DataContext = _productsFromXml;
        }
    }
}

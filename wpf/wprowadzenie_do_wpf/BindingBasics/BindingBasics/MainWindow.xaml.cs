﻿using System;
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

namespace YourAppName
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Produkt p1 = null;

        public MainWindow()
        {
            InitializeComponent();
            CreateDataContext();
        }

        private void CreateDataContext()
        {
            p1 = new Produkt("DZ-10", "spasiona kurwiej", 150, "Wscielej dupy");
            GridOfProduct.DataContext = p1;

        }

        private void btnPotwierdz_Click(object sender, RoutedEventArgs e)
        {
            string tekst = String.Format("{0}{1}{2}", "Wprowadzono dane:", Environment.NewLine, p1.ToString()); MessageBox.Show(tekst);
        }
    }
}

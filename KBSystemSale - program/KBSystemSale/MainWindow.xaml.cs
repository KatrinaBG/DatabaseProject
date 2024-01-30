using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KBSystemSale.Peages;
namespace KBSystemSale
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String aktywnaStrona = null;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {

            frame1.Source = new Uri("/Peages/Pracownicy.xaml", UriKind.Relative);
            aktywnaStrona = "pracownicy";
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            frame1.Source = new Uri("/Peages/Towary.xaml", UriKind.Relative);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            frame1.Source = new Uri("/Peages/Kontrahenci.xaml", UriKind.Relative);
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            frame1.Source = new Uri("/Peages/Sprzedaz.xaml", UriKind.Relative);
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            frame1.Source = new Uri("/Peages/Magazyn.xaml", UriKind.Relative);
        }

        

        

       
    }
}

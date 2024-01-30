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
using System.Windows.Shapes;

namespace KBSystemSale.Windows
{
    /// <summary>
    /// Interaction logic for DodajPracownika.xaml
    /// </summary>
    public partial class DodajPracownika : Window
    {
        private Test_kkEntities dbcontext;

        public DodajPracownika() : this(new Test_kkEntities()) { }

        public DodajPracownika(Test_kkEntities dbcontext)
        {
            InitializeComponent();
            this.dbcontext = dbcontext;
            adrescb.ItemsSource = dbcontext.adres;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

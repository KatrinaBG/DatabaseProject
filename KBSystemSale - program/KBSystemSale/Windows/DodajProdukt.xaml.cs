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
    /// Interaction logic for DodajProdukt.xaml
    /// </summary>
    public partial class DodajProdukt : Window
    {
        private Test_kkEntities dbcontext;
        public DodajProdukt()
            : this(new Test_kkEntities())
        {
        }

        public DodajProdukt(Test_kkEntities dbcontext)
        {
            InitializeComponent();
            this.dbcontext = dbcontext;
            prodcbx.ItemsSource = dbcontext.producent;
            katcbx.ItemsSource = dbcontext.kategoria;
            promcbx.ItemsSource = dbcontext.promocja;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

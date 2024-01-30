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
using System.ComponentModel;
using System.Collections.ObjectModel;
using KBSystemSale.Windows;
using System.Data.Objects;

namespace KBSystemSale.Peages
{

    /// <summary>
    /// Interaction logic for Towary.xaml
    /// </summary>
    public partial class Towary : Page
    {

        private ICollectionView view;
        private Test_kkEntities dbcontext;
        public Towary()
        {
            InitializeComponent();
            dataGrid1.SelectionChanged += new SelectionChangedEventHandler(dataGrid1_SelectionChanged);
            dbcontext = new Test_kkEntities();
            Refresh();
        }

        private void Refresh()
        {
            view = CollectionViewSource.GetDefaultView(new ObservableCollection<produkty>(dbcontext.produkty));
            dataGrid1.ItemsSource = view;
            Filter();
        }

        private void Filter()
        {
            var searchPhrase = searchtbx.Text.Trim();
            if (view.CanFilter && !String.IsNullOrEmpty(searchPhrase))
            {
                if (prodradio.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as produkty;
                        if (i.producent.kontrahenci.Nazwa.Contains(searchPhrase))
                        {
                            return true;
                        }
                        return false;
                    });
                }
                else if (katradio.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as produkty;
                        if (i.kategoria.nazwa.Contains(searchPhrase))
                        {
                            return true;
                        }
                        return false;
                    });
                }
                else if (nazwaradio.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as produkty;
                        if (i.model.Contains(searchPhrase))
                        {
                            return true;
                        }
                        return false;
                    });
                }
                else
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        return true;
                    });
                }
            }
            else
            {
                view.Filter = new Predicate<object>((object item) =>
                {
                    return true;
                });
            }
        }

        void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonModyfikuj.IsEnabled = dataGrid1.SelectedItem != null;
        }

     //   public Test_kkEntities dbcontext { get; set; }


        private void searchtbx_TextChanged(object sender, TextChangedEventArgs e)
        {

            Filter();
        }

        private void nazwaradio_Click(object sender, RoutedEventArgs e)
        {

            Filter();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var w = new DodajProdukt(dbcontext);
            w.Title = "Dodaj Produkt";
            w.prodcbx.SelectedItem = null;
            w.modeltbx.Text = "";
            w.cenatbx.Text ="";
            w.stantbx.Text = "";
            w.katcbx.SelectedItem = null;
            w.pntbx.Text = "";
            w.aktywnechbx.IsChecked = true;
            w.promcbx.SelectedItem = null;
            w.opistbx.Text = "";
            w.ActionB.Click += new RoutedEventHandler((object s, RoutedEventArgs ee) =>
            {
                var p = new produkty
                {
                    producent=w.prodcbx.SelectedItem as producent,
                    model =w.modeltbx.Text,
                    cena_katalogowa = decimal.Parse(w.cenatbx.Text),
                    stan_magazynowy=int.Parse(w.stantbx.Text),
                    kategoria=w.katcbx.SelectedItem as kategoria,
                    PN=w.pntbx.Text,
                    aktywne=w.aktywnechbx.IsChecked.Value,
                    opis=w.opistbx.Text,
                    promocja = w.promcbx.SelectedItem as promocja
                };
                dbcontext.produkty.AddObject(p);
                dbcontext.SaveChanges();
                w.Close();
                Refresh();
                
            });
            w.ShowDialog();
        }

        private void buttonModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem as produkty;

            var w = new DodajProdukt(dbcontext);
            w.Title = "Edytuj Produkt";
            w.prodcbx.SelectedItem = item.producent;
            w.modeltbx.Text = item.model;
            w.cenatbx.Text = item.cena_katalogowa.ToString();
            w.stantbx.Text = item.stan_magazynowy.ToString();
            w.katcbx.SelectedItem = item.kategoria;
            w.pntbx.Text = item.PN;
            w.aktywnechbx.IsChecked = item.aktywne;
            w.promcbx.SelectedItem = item.promocja;
            w.opistbx.Text = item.opis;
            w.ActionB.Content = "Zmień";
            w.ActionB.Click += new RoutedEventHandler((object s, RoutedEventArgs ee) =>
            {
                
                    item.producent = w.prodcbx.SelectedItem as producent;
                    item.model = w.modeltbx.Text;
                    item.cena_katalogowa = decimal.Parse(w.cenatbx.Text);
                    item.stan_magazynowy = int.Parse(w.stantbx.Text);
                    item.kategoria = w.katcbx.SelectedItem as kategoria;
                    item.PN = w.pntbx.Text;
                    item.aktywne = w.aktywnechbx.IsChecked.Value;
                    item.opis = w.opistbx.Text;
                    item.promocja = w.promcbx.SelectedItem as promocja;
                
                dbcontext.SaveChanges();
                w.Close();
                Refresh();

            });
            w.ShowDialog();
        }
    }
}
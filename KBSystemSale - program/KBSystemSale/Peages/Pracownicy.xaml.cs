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
    /// Interaction logic for Pracownicy.xaml
    /// </summary>
    public partial class Pracownicy : Page
    {
        private ICollectionView view;
        private Test_kkEntities dbcontext;
         
        public Pracownicy()
        {
            InitializeComponent();
            dataGrid1.SelectionChanged += new SelectionChangedEventHandler(dataGrid1_SelectionChanged);
            dbcontext = new Test_kkEntities();
            Refresh();
            
        }

        void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            buttonModyfikuj.IsEnabled = dataGrid1.SelectedItem != null;
        }
       
         
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void buttonWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(poleWyszukiwania.Text.Trim()) == false && radioButton1.IsChecked == true)
            {
                labelWynik.Content = poleWyszukiwania.Text;
            }
        }

        private void radioButton1_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            var searchPhrase = poleWyszukiwania.Text.Trim();
            if (view.CanFilter && !String.IsNullOrEmpty(searchPhrase) )
            {
                if (radioButton1.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as pracownik;
                        if (i.id_pracownika == searchPhrase)
                        {
                            return true;
                        }
                        return false;
                    });
                }
                else if (radioButton2.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as pracownik;
                        if (i.imie.Contains(searchPhrase))
                        {
                            return true;
                        }
                        return false;
                    });
                }
                else if (radioButton3.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as pracownik;
                        if (i.nazwisko.Contains(searchPhrase))
                        {
                            return true;
                        }
                        return false;
                    });
                }
                else if (radioButton4.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as pracownik;
                        if (i.pesel.Contains(searchPhrase))
                        {
                            return true;
                        }
                        return false;
                    });
                }
                else if (radioButton5.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as pracownik;
                        return i.pracuje;
                    });
                }
                else if (radioButton6.IsChecked.Value)
                {
                    view.Filter = new Predicate<object>((object item) =>
                    {
                        var i = item as pracownik;
                        return !i.pracuje;
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
        public void Refresh()
        {
            view = CollectionViewSource.GetDefaultView(new ObservableCollection<pracownik>(dbcontext.pracownik));
            dataGrid1.ItemsSource = view;
            Filter();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var w = new DodajPracownika(dbcontext);
            w.Title = "Dodaj";
            w.nametb.Text = "";
            w.lnametb.Text = "";
            w.peseltb.Text = "";
            w.adrescb.SelectedItem = null;
            w.workingcb.IsChecked = false;
            w.Commitb.Click += new RoutedEventHandler((object s, RoutedEventArgs ee) =>
            {
                var p = new pracownik
                {
                    id_pracownika=new Random().Next(1111111,9999999).ToString(),
                    imie = w.nametb.Text.Trim(),
                    nazwisko = w.lnametb.Text.Trim(),
                    pesel = w.peseltb.Text.Trim(),
                    adres=w.adrescb.SelectedItem as adres,
                    pracuje=w.workingcb.IsChecked.Value
                };
                dbcontext.pracownik.AddObject(p);
                dbcontext.SaveChanges();
                w.Close();
                Refresh();
            });
            w.ShowDialog();
        }

        private void buttonModyfikuj_Click(object sender, RoutedEventArgs e)
        {
            var item = dataGrid1.SelectedItem as pracownik;
            var w = new DodajPracownika(dbcontext);
            w.Title = "Edytuj";
            w.nametb.Text = item.imie;
            w.lnametb.Text = item.nazwisko;
            w.peseltb.Text = item.pesel;
            w.adrescb.SelectedItem = (w.adrescb.ItemsSource as ObjectSet<adres>).First(i => i.id_adres==item.adres.id_adres);
            w.workingcb.IsChecked = item.pracuje;
            w.Commitb.Click += new RoutedEventHandler((object s, RoutedEventArgs ee) =>
            {
                    item.imie = w.nametb.Text.Trim();
                    item.nazwisko = w.lnametb.Text.Trim();
                    item.pesel = w.peseltb.Text.Trim();
                    item.adres = w.adrescb.SelectedItem as adres;
                    item.pracuje = w.workingcb.IsChecked.Value;
                
                
                dbcontext.SaveChanges();
                w.Close();
                Refresh();
            });
            w.ShowDialog();
        }



        
      
    }
}

﻿using dip.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace dip.pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private ObservableCollection<merch> merchCollection;
        public Admin()
        {
            InitializeComponent();
            
            merchCollection = new ObservableCollection<merch>(dipEntitie.GetContext().merch.ToList());
            BDWorkers.ItemsSource = merchCollection;
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            AddMerches add = new AddMerches((sender as Button).DataContext as merch);
            Visibility = Visibility.Hidden;
            add.Show();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var merchDell = BDWorkers.SelectedItems.Cast<merch>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {merchDell.Count()} элементов?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    IEnumerable<merch> enumerable = dipEntitie.GetContext().merch.RemoveRange((IEnumerable<merch>)merchDell);
                    dipEntitie.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    BDWorkers.ItemsSource = dipEntitie.GetContext().merch.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            AddMerches add = new AddMerches(null);
            this.Visibility = Visibility.Hidden;
            add.Show();
        }
        private void RefreshPage()
        {
            merchCollection.Clear();
            foreach (var merch in dipEntitie.GetContext().merch.ToList())
            {
                merchCollection.Add(merch);
            }
        }

        private void obnovClick(object sender, RoutedEventArgs e)
        {
           
                RefreshPage();
        }

        private void prosmClick(object sender, RoutedEventArgs e)
        {
            prosmZak prosm = new prosmZak();
            Visibility = Visibility.Hidden;
            prosm.Show();
        }

        private void LogHis(object sender, RoutedEventArgs e)
        {
            LogHistory log = new LogHistory();
            Visibility = Visibility.Hidden;
            log.Show();
        }

        private void AutClick(object sender, RoutedEventArgs e)
        {
            autor aut = new autor();
            Visibility = Visibility.Hidden;
            aut.Show();

        }
    }
}

using dip.models;
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
using System.Windows.Shapes;

namespace dip.pages
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();
            BDWorkers.ItemsSource = dipEntities.GetContext().merch.ToList();
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
                    IEnumerable<merch> enumerable = dipEntities.GetContext().merch.RemoveRange((IEnumerable<merch>)merchDell);
                    dipEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    BDWorkers.ItemsSource = dipEntities.GetContext().merch.ToList();
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
    }
}

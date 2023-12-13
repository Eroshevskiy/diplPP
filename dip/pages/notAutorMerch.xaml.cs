using dip.models;
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
using static dip.pages.merches;

namespace dip.pages
{
    /// <summary>
    /// Логика взаимодействия для notAutorMerch.xaml
    /// </summary>
    public partial class notAutorMerch : Window
    {
        private ObservableCollection<OrderItem> orderItems = new ObservableCollection<OrderItem>();
        private ObservableCollection<merch> merchCollection;
        private int _currentPage = 1;
        private int _countInPage = 3;
        public notAutorMerch()
        {
            InitializeComponent();
            lv.ItemsSource = models.dipEntitie.GetContext().merch.ToList();
            merchCollection = new ObservableCollection<merch>(dipEntitie.GetContext().merch.ToList());
            UpdateMerchCollection();
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                UpdateMerchCollection();
            }
        }
        private void UpdateMerchCollection()
        {
            int startIndex = (_currentPage - 1) * _countInPage;
            var itemsForPage = merchCollection.Skip(startIndex).Take(_countInPage).ToList();
            lv.ItemsSource = itemsForPage;
        }

        private void mainClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Visibility = Visibility.Hidden;
            main.Show();
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            int totalItems = merchCollection.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / _countInPage);

            if (_currentPage < totalPages)
            {
                _currentPage++;
                UpdateMerchCollection();
            }
        }
    }
}

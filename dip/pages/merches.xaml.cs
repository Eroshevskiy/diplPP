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

namespace dip.pages
{
    /// <summary>
    /// Логика взаимодействия для merches.xaml
    /// </summary>
    public partial class merches : Window
    {
        private ObservableCollection<OrderItem> orderItems = new ObservableCollection<OrderItem>();
        private ObservableCollection<merch> merchCollection;
        private int _currentPage = 1;
        private int _countInPage = 3;
        public merches()
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

        private void UpdateOrderViewButtonVisibility()
        {
            if (orderItems.Any())
            {
                ShowOrderButton.Visibility = Visibility.Visible;
            }
            else
            {
                ShowOrderButton.Visibility = Visibility.Collapsed;
            }
        }



        

        private void MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lv.SelectedItem == null)
            {
                e.Handled = true;
            }
            else
            {
                merch selectedMerch = lv.SelectedItem as merch;
                MessageBox.Show($"Selected Merch:\nName: {selectedMerch.name}\nManufacturer: {selectedMerch.manufacturer}\nPrice: {selectedMerch.price}", "Merchandise Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (lv.SelectedItem != null)
            {
                merch selectedMerch = lv.SelectedItem as merch;

                OrderItem orderItem = new OrderItem
                {
                    MerchId = selectedMerch.id,
                    MerchName = selectedMerch.name,
                    MerchPrice = selectedMerch.price,
                    Quantity = 1,
                    PointCollection = new ObservableCollection<point>()
                };

                if (selectedMerch.discount.HasValue)
                {
                    orderItem.Discount = selectedMerch.discount.Value;
                }
                else
                {
                    MessageBox.Show("У товара нет скидки");
                }

                orderItems.Add(orderItem);

                UpdateOrderViewButtonVisibility();
            }
        }

        private void UpdateMerchCollection()
        {
            int startIndex = (_currentPage - 1) * _countInPage;
            var itemsForPage = merchCollection.Skip(startIndex).Take(_countInPage).ToList();
            lv.ItemsSource = itemsForPage;
        }

        private void ShowOrderButton_Click(object sender, RoutedEventArgs e)
        {
            OrerClient orderViewWindow = new OrerClient(orderItems);
            orderViewWindow.ShowDialog();
        }
        public class OrderItem
        {
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public int StatusId { get; set; }
            public int PointId { get; set; }
            public DateTime OrderDate { get; set; }
            public int Code { get; set; }
            public int Cost { get; set; }
            public int Discount { get; set; }
            public int MerchId { get; set; }
            public string MerchName { get; set; }
            public decimal MerchPrice { get; set; }
            public int Quantity { get; set; }
            public ObservableCollection<point> PointCollection { get; set; }
            public string SelectedPoint { get; set; }
        }

        

        private void userClick(object sender, RoutedEventArgs e)
        {
            User us = new User();
            Visibility = Visibility.Hidden;
            us.Show();

        }
    }
}

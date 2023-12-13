using dip.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// Логика взаимодействия для prosmZak.xaml
    /// </summary>
    public partial class prosmZak : Window
    {
        public prosmZak()
        {
            InitializeComponent();
            List<orders> ordersList = dipEntitie.GetContext().orders.ToList();
            dataGrid.ItemsSource = ordersList;
        }

        

        private void QrSave(object sender, RoutedEventArgs e)
        {
            QR qr = new QR();
            Visibility = Visibility.Hidden;
            qr.Show();
        }

        private void SavePdf(object sender, RoutedEventArgs e)
        {
            try
            {
                PrintDialog dialog = new PrintDialog();

                if (dialog.ShowDialog() != true)
                    return;

                // Устанавливаем размеры страницы (например, в миллиметрах)
                dialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
                dialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

                

                // Печать содержимого DataGrid
                dialog.PrintVisual(dataGrid, "Печать отчета");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Печать отчета", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditOrder(object sender, RoutedEventArgs e)
        {
            EdirOrders edit = new EdirOrders((sender as Button).DataContext as orders);
            Visibility = Visibility.Hidden;
            edit.Show();
        }

        private void nazClick(object sender, RoutedEventArgs e)
        {
            Admin adm = new Admin();
            Visibility = Visibility.Hidden;
            adm.Show();
        }
    }
}

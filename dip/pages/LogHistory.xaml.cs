using dip.classes;
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
    /// Логика взаимодействия для LogHistory.xaml
    /// </summary>
    public partial class LogHistory : Window
    {
        private ObservableCollection<vhodHis> _filteredvhodHis;
        public LogHistory()
        {
            InitializeComponent();
            LoadLoginHistory();
        }
        private void LoadLoginHistory()
        {
            try
            {
                // Получаем историю входов из базы данных
                var loginHistory = dboconnect.modeldb.vhodHis.ToList();

                // Используем ObservableCollection для автоматического обновления интерфейса при изменении коллекции
                _filteredvhodHis = new ObservableCollection<vhodHis>(loginHistory);

                // Отображаем историю входов на странице
                LoginHistoryDataGrid.ItemsSource = _filteredvhodHis;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке истории входов: " + ex.Message.ToString(),
                                "Критическая работа приложения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void SuccessCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void SuccessCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            // Получаем изначальную коллекцию
            var originalCollection = dboconnect.modeldb.vhodHis.ToList();

            // Фильтруем
            var filteredCollection = originalCollection
                .Where(entry => string.IsNullOrEmpty(SearchTextBox.Text) || (entry.users != null && entry.users.login.Contains(SearchTextBox.Text)))
                .ToList();

            if (SuccessCheckBox.IsChecked == true)
            {
                // Фильтруем только успешные входы
                filteredCollection = filteredCollection
                    .Where(entry => entry.TypeVhod == "Успешно")
                    .ToList();
            }

            // Обновляем отображаемую коллекцию
            _filteredvhodHis.Clear();
            foreach (var item in filteredCollection)
            {
                _filteredvhodHis.Add(item);
            }
        }

        private void sbrosFilters(object sender, RoutedEventArgs e)
        {
            LoadLoginHistory();
            
            SearchTextBox.Text = string.Empty;
            SuccessCheckBox.IsChecked = false;
        }

        private void nazClick(object sender, RoutedEventArgs e)
        {
            Admin adm = new Admin();
            Visibility = Visibility.Hidden;
            adm.Show();
        }
    }
}

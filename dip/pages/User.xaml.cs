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
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window
    {
        public User()
        {
            InitializeComponent();
        }

        private void mainClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Visibility = Visibility.Hidden;
            main.Show();
        }

       

        private void infoClick(object sender, RoutedEventArgs e)
        {
            info inf = new info();
            Visibility = Visibility.Hidden;
            inf.Show();
        }

        private void contClick(object sender, RoutedEventArgs e)
        {
            contacts con = new contacts();
            Visibility = Visibility.Hidden;
            con.Show();
        }

        private void merchClick(object sender, RoutedEventArgs e)
        {
            merches merc = new merches();
            Visibility = Visibility.Hidden;
            merc.Show();
        }
    }
}

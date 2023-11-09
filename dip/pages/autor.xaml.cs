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
    /// Логика взаимодействия для autor.xaml
    /// </summary>
    public partial class autor : Window
    {
        public autor()
        {
            InitializeComponent();
            classes.dboconnect.modeldb = new models.dipEntities();
        }
        private void Enter(object sender, RoutedEventArgs e)
        {


            var userobj = classes.dboconnect.modeldb.users.FirstOrDefault(x =>
            login.Text == x.login && password.Password == x.password);

            if (userobj.id_type == 1)
            {
                Admin admin = new Admin();
                this.Visibility = Visibility.Hidden;
                admin.Show();
            }

            if (userobj.id_type == 2)
            {
                Manager manager = new Manager();
                this.Visibility = Visibility.Hidden;
                manager.Show();

            }

            if (userobj.id_type == 3)
            {
                User users = new User();
                this.Visibility = Visibility.Hidden;
                users.Show();

            }

        }

        private void mainClick(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            Visibility = Visibility.Hidden;
            main.Show();
        }
    }
}

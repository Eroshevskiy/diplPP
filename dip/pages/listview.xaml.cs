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
    /// Логика взаимодействия для listview.xaml
    /// </summary>
    public partial class listview : Window
    {
        public listview()
        {
            InitializeComponent();
            lv.ItemsSource = models.dipEntitie.GetContext().merch.ToList();

        }
    }
}

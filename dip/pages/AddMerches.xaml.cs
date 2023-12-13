using dip.models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для AddMerches.xaml
    /// </summary>
    public partial class AddMerches : Window
    {
        private merch currentmerch = new merch();
        public OpenFileDialog ofd = new OpenFileDialog();
        private string newsourthpath = string.Empty;
        private bool flag = false;
        public AddMerches(merch sellectedmer)
        {
            InitializeComponent();
            if (sellectedmer != null)
            {
                currentmerch = sellectedmer;
            }
            DataContext = currentmerch;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (currentmerch.id == 0)
            {
                dipEntitie.GetContext().merch.Add(currentmerch);
            }

            DbContextTransaction dbContextTransaction = null;

            try
            {
                if (currentmerch.id == 0)
                {
                    dipEntitie.GetContext().merch.Add(currentmerch);
                }

                dbContextTransaction = dipEntitie.GetContext().Database.BeginTransaction();

                dipEntitie.GetContext().SaveChanges();

                MessageBox.Show("Информация сохранена!");
                dbContextTransaction.Commit();

            }
            catch (DbUpdateException ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }

                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    MessageBox.Show($"Внутреннее исключение: {innerException.Message}");
                    innerException = innerException.InnerException;
                }

                MessageBox.Show("Ошибка при сохранении изменений. Дополнительные сведения в внутреннем исключении.");
            }
            catch (Exception ex)
            {
                if (dbContextTransaction != null)
                {
                    dbContextTransaction.Rollback();
                }

                MessageBox.Show($"Ошибка при обновлении записей. Дополнительные сведения: {ex.Message}");
            }
            finally
            {
                dbContextTransaction?.Dispose();
            }
        }

        
        private void Foto(object sender, RoutedEventArgs e)
        {
            string source = Environment.CurrentDirectory;
            if (ofd.ShowDialog() == true)
            {
                flag = true;
                string sourthpath = ofd.SafeFileName;
                newsourthpath = System.IO.Path.Combine(source.Replace("/bin/Debug", "/photo/"), sourthpath);
                // Проверка на null перед установкой изображения
                if (ofd.FileName != null)
                {
                    PreviewImage.Source = new BitmapImage(new Uri(ofd.FileName));
                }
                currentmerch.photo = $"/photo/{ofd.SafeFileName}";
            }
        }

        private void nazClick(object sender, RoutedEventArgs e)
        {
            Admin adm = new Admin();
            Visibility = Visibility.Hidden;
            adm.Show();
        }
    }
}

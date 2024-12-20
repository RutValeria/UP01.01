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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UP01._01.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        public RequestsPage()
        {
            InitializeComponent();
            DataGridRequests.ItemsSource = Entities.GetContext().Requests.ToList();
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new Pages.AddRequestPage(null));
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DataGridRequests.SelectedItems.Cast<Requests>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {usersForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                try
                {
                    Entities.GetContext().Requests.RemoveRange(usersForRemoving);
                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены");

                    DataGridRequests.ItemsSource = Entities.GetContext().Requests.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
        }
        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridRequests.ItemsSource = Entities.GetContext().Requests.ToList();
            }
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.AddRequestPage((sender as Button).DataContext as Requests));
        }
    }
}

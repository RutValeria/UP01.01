using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для AddRequestPage.xaml
    /// </summary>
    public partial class AddRequestPage : Page
    {
        private Entities context;
        private Requests _currentRequests = new Requests();
        public AddRequestPage(object value)
        {
            InitializeComponent();
            DataContext = _currentRequests;
            context = new Entities();
            LoadComboBoxes();
        }
        private void LoadComboBoxes()
        {
            CmbEquipment.ItemsSource = context.Equipment.ToList();
            CmbEquipment.DisplayMemberPath = "EquipmentName";
            CmbEquipment.SelectedValuePath = "EquipmentID";

            CmbProblem.ItemsSource = context.Problems.ToList();
            CmbProblem.DisplayMemberPath = "Description";
            CmbProblem.SelectedValuePath = "ProblemID";

            CmbClient.ItemsSource = context.Clients.ToList();
            CmbClient.DisplayMemberPath = "Name";
            CmbClient.SelectedValuePath = "ClientID";

            CmbStatus.ItemsSource = context.RequestStatuses.ToList();
            CmbStatus.DisplayMemberPath = "StatusName";
            CmbStatus.SelectedValuePath = "StatusID";

            CmbStaff.ItemsSource = context.Staff.ToList();
            CmbStaff.DisplayMemberPath = "StaffName";
            CmbStaff.SelectedValuePath = "StaffID";
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            // Проверяем переменную errors на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            // Добавляем в объект Requests новую запись
            if (_currentRequests.RequestID == 0)
                Entities.GetContext().Requests.Add(_currentRequests);
            // Делаем попытку записи данных в БД о новом пользователе
            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RequestsPage());
        }
    }
}

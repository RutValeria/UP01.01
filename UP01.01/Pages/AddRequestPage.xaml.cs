using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace UP01._01.Pages
{
	public partial class AddRequestPage : Page
	{
		private Requests _currentRequests = new Requests();
		public bool flag = true;

		public AddRequestPage(Requests selectedRequests)
		{
			InitializeComponent();

			if (selectedRequests != null)
			{
				flag = false;
				_currentRequests = selectedRequests;
				CmbClient.SelectedIndex = selectedRequests.ClientID - 1;
				CmbEquipment.SelectedIndex = selectedRequests.EquipmentID - 1;
				CmbProblem.SelectedIndex = selectedRequests.ProblemID - 1;
				CmbStaff.SelectedIndex = selectedRequests.StaffID - 1;
				CmbStatus.SelectedIndex = selectedRequests.StatusID - 1;
			}

			DataContext = _currentRequests;

			var context = Entities.GetContext();
			CmbClient.ItemsSource = context.Clients.Distinct().ToList();
			CmbEquipment.ItemsSource = context.Equipment.Distinct().ToList();
			CmbProblem.ItemsSource = context.Problems.Distinct().ToList();
			CmbStaff.ItemsSource = context.Staff.Distinct().ToList();
			CmbStatus.ItemsSource = context.RequestStatuses.Distinct().ToList();
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result;

			if (flag)
			{
				result = MessageBox.Show("Вы уверены что хотите добавить эти данные?", "Подтвержение добавления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
				if (result == MessageBoxResult.Yes)
				{
					using (Entities db = new Entities())
					{
						Requests newRequest = new Requests
						{
							RequestDate = DateTime.Parse(TBDate.Text),
							EquipmentID = CmbEquipment.SelectedIndex + 1,
							ProblemID = CmbProblem.SelectedIndex + 1,
							ProblemDescription = TBOpis.Text,
							ClientID = CmbClient.SelectedIndex + 1,
							StatusID = CmbStatus.SelectedIndex + 1,
							StaffID = CmbStaff.SelectedIndex + 1,
						};

						db.Requests.Add(newRequest);
						db.SaveChanges();
						MessageBox.Show("Заявка добавлена");
					}
				}
			}
			else
			{
				result = MessageBox.Show("Вы уверены что хотите сохранить эти данные?", "Подтвержение сохранения", MessageBoxButton.YesNo, MessageBoxImage.Warning);
				if (result == MessageBoxResult.Yes)
				{
					try
					{
						using (Entities db = new Entities())
						{
							// Обновляем существующий запрос
							var requestToUpdate = db.Requests.Find(_currentRequests.RequestID); // Предполагаем, что у вас есть ID
							if (requestToUpdate != null)
							{
								requestToUpdate.RequestDate = DateTime.Parse(TBDate.Text);
								requestToUpdate.EquipmentID = CmbEquipment.SelectedIndex + 1;
								requestToUpdate.ProblemID = CmbProblem.SelectedIndex + 1;
								requestToUpdate.ProblemDescription = TBOpis.Text;
								requestToUpdate.ClientID = CmbClient.SelectedIndex + 1;
								requestToUpdate.StatusID = CmbStatus.SelectedIndex + 1;
								requestToUpdate.StaffID = CmbStaff.SelectedIndex + 1;
								db.SaveChanges();
								MessageBox.Show("Данные успешно сохранены!");
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
					}
				}
			}

			NavigationService.GoBack();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			NavigationService?.Navigate(new RequestsPage());
		}
	}
}
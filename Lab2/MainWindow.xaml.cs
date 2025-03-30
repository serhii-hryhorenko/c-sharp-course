using Lab2;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Threading.Tasks;

namespace Lab2
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            FirstNameTextBox.TextChanged += ValidateInput;
            LastNameTextBox.TextChanged += ValidateInput;
            EmailTextBox.TextChanged += ValidateInput;
            DateOfBirthPicker.DateChanged += ValidateInput;
        }

        // �������� �� ���������� ��� ����
        private void ValidateInput(object sender, object e)
        {
            ProceedButton.IsEnabled = !string.IsNullOrEmpty(FirstNameTextBox.Text) &&
                                      !string.IsNullOrEmpty(LastNameTextBox.Text) &&
                                      !string.IsNullOrEmpty(EmailTextBox.Text) &&
                                      DateOfBirthPicker.Date != null;
        }

        private async void OnProceedClicked(object sender, RoutedEventArgs e)
        {
            ProceedButton.IsEnabled = false;
            var person = new Person(
                FirstNameTextBox.Text,
                LastNameTextBox.Text,
                EmailTextBox.Text,
                DateOfBirthPicker.Date.DateTime);

            await DisplayPersonDataAsync(person);

            ProceedButton.IsEnabled = true;
        }

        private void BlockUIElements(bool block)
        {
            FirstNameTextBox.IsEnabled = !block;
            LastNameTextBox.IsEnabled = !block;
            EmailTextBox.IsEnabled = !block;
            DateOfBirthPicker.IsEnabled = !block;

            ProceedButton.IsEnabled = !block;
        }

        private async Task DisplayPersonDataAsync(Person person)
        {
            BlockUIElements(true);
            await Task.Delay(2000);

            string output = $@"
                ��'�: {person.FirstName}
                �������: {person.LastName}
                ���������� ������: {person.Email}
                ���� ����������: {person.DateOfBirth.ToShortDateString()}
                �� �������: {person.IsAdult}
                �������� ����: {person.SunSign}
                ���������� ����: {person.ChineseSign}
                �� ������� ���� ����������: {person.IsBirthday}";

            ContentDialog dialog = new ContentDialog
            {
                Title = "����������",
                Content = output,
                CloseButtonText = "OK"
            };

            dialog.XamlRoot = this.Content.XamlRoot;
            await dialog.ShowAsync();

            BlockUIElements(false);
        }
    }
}

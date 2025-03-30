using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lab1;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Lab1.ViewModel
{
    public class BitrhdayViewModel : INotifyPropertyChanged
    {
        private DateTimeOffset? _birthDate;
        private string _age;
        private string _westernZodiac;
        private string _chineseZodiac;
        private string _message;

        public DateTimeOffset? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
                CalculateAgeAndZodiac();
            }
        }

        public string Age
        {
            get => _age;
            private set { _age = value; OnPropertyChanged(); }
        }

        public string WesternZodiac
        {
            get => _westernZodiac;
            private set { _westernZodiac = value; OnPropertyChanged(); }
        }

        public string ChineseZodiac
        {
            get => _chineseZodiac;
            private set { _chineseZodiac = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => _message;
            private set { _message = value; OnPropertyChanged(); }
        }

        private async void CalculateAgeAndZodiac()
        {
            if (!BirthDate.HasValue)
                return;

            DateTime today = DateTime.Today;
            int age = today.Year - BirthDate.Value.Year;
            if (BirthDate.Value.Date > today.AddYears(-age))
                age--;

            if (age < 0 || age > 135)
            {
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Помилка",
                    Content = "Невірний вік! Будь ласка, введіть коректну дату народження.",
                    CloseButtonText = "OK",
                    XamlRoot = App.MainWindow.Content.XamlRoot
                };
                await dialog.ShowAsync();
                return;
            }

            Age = $"Ваш вік: {age} років";
            WesternZodiac = $"Західний знак зодіаку: {GetWesternZodiac(BirthDate.Value.DateTime)}";
            ChineseZodiac = $"Китайський знак зодіаку: {GetChineseZodiac(BirthDate.Value.DateTime)}";
            Message = BirthDate.Value.Month == today.Month && BirthDate.Value.Day == today.Day
                ? "З Днем народження! 🎉"
                : "";
        }

        private string GetWesternZodiac(DateTime date)
        {
            int day = date.Day, month = date.Month;
            return month switch
            {
                1 when day >= 20 => "Водолій",
                1 => "Козеріг",
                2 when day >= 19 => "Риби",
                2 => "Водолій",
                3 when day >= 21 => "Овен",
                3 => "Риби",
                4 when day >= 20 => "Телець",
                4 => "Овен",
                5 when day >= 21 => "Близнюки",
                5 => "Телець",
                6 when day >= 21 => "Рак",
                6 => "Близнюки",
                7 when day >= 23 => "Лев",
                7 => "Рак",
                8 when day >= 23 => "Діва",
                8 => "Лев",
                9 when day >= 23 => "Терези",
                9 => "Діва",
                10 when day >= 23 => "Скорпіон",
                10 => "Терези",
                11 when day >= 22 => "Стрілець",
                11 => "Скорпіон",
                12 when day >= 22 => "Козеріг",
                _ => "Стрілець"
            };
        }

        private string GetChineseZodiac(DateTime date)
        {
            string[] chineseZodiac = { "Мавпа", "Півень", "Собака", "Свиня", "Щур", "Бик", "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза" };
            return chineseZodiac[date.Year % 12];
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

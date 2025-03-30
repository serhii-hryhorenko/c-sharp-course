using System;

namespace Lab2
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime DateOfBirth { get; }

        public Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, DateTime.MinValue)
        { }

        public Person(string firstName, string lastName, DateTime dateOfBirth)
            : this(firstName, lastName, string.Empty, dateOfBirth)
        { }

        public bool IsAdult => (DateTime.Now - DateOfBirth).TotalDays >= 18 * 365.25;

        public string SunSign => GetSunSign(DateOfBirth);

        public string ChineseSign => GetChineseSign(DateOfBirth);

        public bool IsBirthday => DateOfBirth.Month == DateTime.Now.Month && DateOfBirth.Day == DateTime.Now.Day;

        private string GetSunSign(DateTime date)
        {
            var zodiacSigns = new[]
            {
                new { Sign = "Capricorn", Start = new DateTime(date.Year, 12, 22), End = new DateTime(date.Year + 1, 1, 19) },
                new { Sign = "Aquarius", Start = new DateTime(date.Year, 1, 20), End = new DateTime(date.Year, 2, 18) },
                new { Sign = "Pisces", Start = new DateTime(date.Year, 2, 19), End = new DateTime(date.Year, 3, 20) },
                new { Sign = "Aries", Start = new DateTime(date.Year, 3, 21), End = new DateTime(date.Year, 4, 19) },
                new { Sign = "Taurus", Start = new DateTime(date.Year, 4, 20), End = new DateTime(date.Year, 5, 20) },
                new { Sign = "Gemini", Start = new DateTime(date.Year, 5, 21), End = new DateTime(date.Year, 6, 20) },
                new { Sign = "Cancer", Start = new DateTime(date.Year, 6, 21), End = new DateTime(date.Year, 7, 22) },
                new { Sign = "Leo", Start = new DateTime(date.Year, 7, 23), End = new DateTime(date.Year, 8, 22) },
                new { Sign = "Virgo", Start = new DateTime(date.Year, 8, 23), End = new DateTime(date.Year, 9, 22) },
                new { Sign = "Libra", Start = new DateTime(date.Year, 9, 23), End = new DateTime(date.Year, 10, 22) },
                new { Sign = "Scorpio", Start = new DateTime(date.Year, 10, 23), End = new DateTime(date.Year, 11, 21) },
                new { Sign = "Sagittarius", Start = new DateTime(date.Year, 11, 22), End = new DateTime(date.Year, 12, 21) }
            };

            foreach (var zodiac in zodiacSigns)
            {
                if (date >= zodiac.Start && date <= zodiac.End)
                {
                    return zodiac.Sign;
                }
            }

            return "Unknown"; // Should never happen
        }

        private string GetChineseSign(DateTime date)
        {
            var chineseZodiac = new[]
            {
                "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig"
            };

            int year = date.Year;
            int index = (year - 4) % 12;

            return chineseZodiac[index];
        }
    }
}

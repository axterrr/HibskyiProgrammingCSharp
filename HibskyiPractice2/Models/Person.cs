using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice2.Models
{
    internal class Person
    {
        private string _firstName;
        private string _lastName;
        private string? _email;
        private DateTime? _dateOfBirth;
        private bool? _isAdult;
        private string? _sunSign;
        private string? _chineseSign;
        private bool? _isBirthday;

        public Person(string firstName, string lastName, string? email, DateTime? dateOfBirth)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _dateOfBirth = dateOfBirth;
            CalculateData();
        }

        public Person(string firstName, string lastName, string email) : 
            this(firstName, lastName, email, null) {}
        public Person(string firstName, string lastName, DateTime dateOfBirth) : 
            this(firstName, lastName, null, dateOfBirth) {}

        public string FirstName 
        { 
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string? Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public bool? IsAdult
        {
            get { return _isAdult; }
        }

        public string? SunSign
        {
            get { return _sunSign; }
        }

        public string? ChineseSign
        {
            get { return _chineseSign; }
        }

        public bool? IsBirthday
        {
            get { return _isBirthday; }
        }

        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                CalculateData();
            }
        }

        private void CalculateData()
        {
            if (DateOfBirth == null)
            {
                _isAdult = null;
                _sunSign = null;
                _chineseSign = null;
                _isBirthday = null;
                return;
            }

            _isAdult = CalculateIsAdult();
            _sunSign = CalculateSunSign();
            _chineseSign = CalculateChineseSign();
            _isBirthday = CalculateIsBirthday();
        }

        private bool CalculateIsAdult()
        {
            int age = DateTime.Today.Year - DateOfBirth.Value.Year;
            if (DateTime.Today < DateOfBirth.Value.AddYears(age))
                --age;

            if (age < 0 || age > 135)
                throw new ArgumentException("Incorrect date of birth!");

            return (age >= 18);
        }

        private bool CalculateIsBirthday()
        {
            return (DateOfBirth.Value.Day == DateTime.Today.Day && DateOfBirth.Value.Month == DateTime.Today.Month);
        }

        private string? CalculateSunSign()
        {
            if (DateOfBirth == null)
                return null;

            int day = DateOfBirth.Value.Day;
            int month = DateOfBirth.Value.Month;

            return month switch
            {
                1 => day <= 20 ? "Capricorn" : "Aquarius",
                2 => day <= 19 ? "Aquarius" : "Pisces",
                3 => day <= 20 ? "Pisces" : "Aries",
                4 => day <= 20 ? "Aries" : "Taurus",
                5 => day <= 20 ? "Taurus" : "Gemini",
                6 => day <= 21 ? "Gemini" : "Cancer",
                7 => day <= 22 ? "Cancer" : "Leo",
                8 => day <= 23 ? "Leo" : "Virgo",
                9 => day <= 22 ? "Virgo" : "Libra",
                10 => day <= 23 ? "Libra" : "Scorpio",
                11 => day <= 22 ? "Scorpio" : "Sagittarius",
                12 => day <= 21 ? "Sagittarius" : "Capricorn",
                _ => null,
            };
        }

        private string? CalculateChineseSign()
        {
            if (DateOfBirth == null)
                return null;

            return (DateOfBirth.Value.Year % 12) switch
            {
                0 => "Monkey",
                1 => "Rooster",
                2 => "Dog",
                3 => "Pig",
                4 => "Rat",
                5 => "Ox",
                6 => "Tiger",
                7 => "Rabbit",
                8 => "Dragon",
                9 => "Snake",
                10 => "Horse",
                11 => "Goat",
                _ => null,
            };
        }

    }
}

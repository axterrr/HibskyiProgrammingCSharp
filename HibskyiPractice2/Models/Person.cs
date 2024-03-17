using KMA.ProgrammingCSharp.HibskyiPractice2.Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
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
            set 
            {
                if (value != null && !Regex.IsMatch(value, "^[a-z0-9\\.\\-_]+@[a-z]+(\\.[a-z]+)+$"))
                    throw new InvalidEmailException(value);
                _email = value;
            }
        }

        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value == null)
                {
                    _dateOfBirth = null;
                    _isAdult = null;
                    _sunSign = null;
                    _chineseSign = null;
                    _isBirthday = null;
                    return;
                }

                int age = CalculateAge(value.Value);
                if (age < 0)
                    throw new FutureDateOfBirthException(value.Value.ToString("dd.MM.yyyy"));
                if (age > 135)
                    throw new AncientDateOfBirthException(value.Value.ToString("dd.MM.yyyy"));

                _dateOfBirth = value;
                _isAdult = (age >= 18);
                _sunSign = CalculateSunSign(value.Value);
                _chineseSign = CalculateChineseSign(value.Value);
                _isBirthday = CalculateIsBirthday(value.Value);
            }
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

        private static int CalculateAge(DateTime dt)
        {
            int age = DateTime.Today.Year - dt.Year;
            if (DateTime.Today < dt.AddYears(age))
                --age;
            return age;
        }

        private static bool CalculateIsBirthday(DateTime dt)
        {
            return (dt.Day == DateTime.Today.Day 
                && dt.Month == DateTime.Today.Month);
        }

        private string? CalculateSunSign(DateTime dt)
        {
            int day = dt.Day;
            int month = dt.Month;

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

        private static string? CalculateChineseSign(DateTime dt)
        {
            return (dt.Year % 12) switch
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

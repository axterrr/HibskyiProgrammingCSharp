using KMA.ProgrammingCSharp.HibskyiPractice4.Tools.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Models
{
    internal class Person
    {
        private Guid _guid;
        private string _firstName;
        private string _lastName;
        private string? _email;
        private DateTime? _dateOfBirth;
        private bool? _isAdult;
        private SunSigns? _sunSign;
        private ChineseSigns? _chineseSign;
        private bool? _isBirthday;

        public Person(Guid id, string firstName, string lastName, string? email, DateTime? dateOfBirth)
        {
            Guid = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
        }

        public Person(string firstName, string lastName, string? email, DateTime? dateOfBirth) :
            this(Guid.NewGuid(), firstName, lastName, email, dateOfBirth)
        { }

        public Person(string firstName, string lastName, string email) :
            this(firstName, lastName, email, null)
        { }

        public Person(string firstName, string lastName, DateTime dateOfBirth) :
            this(firstName, lastName, null, dateOfBirth)
        { }

        public Guid Guid
        {
            get { return _guid; }
            set { _guid = value; }
        }

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

        public SunSigns? SunSign
        {
            get { return _sunSign; }
        }

        public ChineseSigns? ChineseSign
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

        private static SunSigns? CalculateSunSign(DateTime dt)
        {
            int day = dt.Day;
            int month = dt.Month;

            return month switch
            {
                1 => day <= 20 ? SunSigns.Capricorn : SunSigns.Aquarius,
                2 => day <= 19 ? SunSigns.Aquarius : SunSigns.Pisces,
                3 => day <= 20 ? SunSigns.Pisces : SunSigns.Aries,
                4 => day <= 20 ? SunSigns.Aries : SunSigns.Taurus,
                5 => day <= 20 ? SunSigns.Taurus : SunSigns.Gemini,
                6 => day <= 21 ? SunSigns.Gemini : SunSigns.Cancer,
                7 => day <= 22 ? SunSigns.Cancer : SunSigns.Leo,
                8 => day <= 23 ? SunSigns.Leo : SunSigns.Virgo,
                9 => day <= 22 ? SunSigns.Virgo : SunSigns.Libra,
                10 => day <= 23 ? SunSigns.Libra : SunSigns.Scorpio,
                11 => day <= 22 ? SunSigns.Scorpio : SunSigns.Sagittarius,
                12 => day <= 21 ? SunSigns.Sagittarius : SunSigns.Capricorn,
                _ => null,
            };
        }

        private static ChineseSigns? CalculateChineseSign(DateTime dt)
        {
            return (dt.Year % 12) switch
            {
                0 => ChineseSigns.Monkey,
                1 => ChineseSigns.Rooster,
                2 => ChineseSigns.Dog,
                3 => ChineseSigns.Pig,
                4 => ChineseSigns.Rat,
                5 => ChineseSigns.Ox,
                6 => ChineseSigns.Tiger,
                7 => ChineseSigns.Rabbit,
                8 => ChineseSigns.Dragon,
                9 => ChineseSigns.Snake,
                10 => ChineseSigns.Horse,
                11 => ChineseSigns.Goat,
                _ => null,
            };
        }

        public enum SunSigns
        {
            Aries,
            Taurus,
            Gemini,
            Cancer,
            Leo,
            Virgo,
            Libra,
            Scorpio,
            Sagittarius,
            Capricorn,
            Aquarius,
            Pisces
        }

        public enum ChineseSigns
        {
            Rat,
            Ox,
            Tiger,
            Rabbit,
            Dragon,
            Snake,
            Horse,
            Goat,
            Monkey,
            Rooster,
            Dog,
            Pig
        }
    }
}

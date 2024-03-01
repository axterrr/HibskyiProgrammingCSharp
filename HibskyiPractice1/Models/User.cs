using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice1.Models
{
    internal class User
    {
        private DateTime _dateOfBirth;
        private int _age;
        private string? _westernZodiacSign;
        private string? _chineseZodiacSign;

        public User()
        {
            DateOfBirth = new DateTime(2001, 1, 1);
        }

        public DateTime DateOfBirth 
        {  
            get 
            { 
                return _dateOfBirth; 
            }
            set 
            {
                if (CalculateAge(value) < 0 || CalculateAge(value) > 135)
                    throw new ArgumentException("Incorrect date of birth!");
                _dateOfBirth = value;
                _age = CalculateAge(value);
                _westernZodiacSign = CalculateWesternZodiacSign();
                _chineseZodiacSign = CalculateChineseZodiacSign();
            } 
        }

        public int Age
        {
            get { return _age; }
        }

        public string? WesternZodiacSign
        {
            get { return _westernZodiacSign; }
        }

        public string? ChineseZodiacSign
        {
            get { return _chineseZodiacSign; }
        }

        public bool CheckBirthday()
        {
            return DateOfBirth.Day == DateTime.Today.Day && DateOfBirth.Month == DateTime.Today.Month;
        }

        private static int CalculateAge(DateTime dt)
        {
            int age = DateTime.Today.Year - dt.Year;
            if (DateTime.Today < dt.AddYears(age)) --age;
            return age;
        }

        private string? CalculateWesternZodiacSign()
        {
            int day = DateOfBirth.Day;
            int month = DateOfBirth.Month;
            switch (month)
            {
                case 1:
                    return day <= 20 ? "Capricorn" : "Aquarius";
                case 2:
                    return day <= 19 ? "Aquarius" : "Pisces";
                case 3:
                    return day <= 20 ? "Pisces" : "Aries";
                case 4:
                    return day <= 20 ? "Aries" : "Taurus";
                case 5:
                    return day <= 20 ? "Taurus" : "Gemini";
                case 6:
                    return day <= 21 ? "Gemini" : "Cancer";
                case 7:
                    return day <= 22 ? "Cancer" : "Leo";
                case 8:
                    return day <= 23 ? "Leo" : "Virgo";
                case 9:
                    return day <= 22 ? "Virgo" : "Libra";
                case 10:
                    return day <= 23 ? "Libra" : "Scorpio";
                case 11:
                    return day <= 22 ? "Scorpio" : "Sagittarius";
                case 12:
                    return day <= 21 ? "Sagittarius" : "Capricorn";
            }
            return null;
        }

        private string? CalculateChineseZodiacSign()
        {
            int year = DateOfBirth.Year;
            switch (year % 12)
            {
                case 0:
                    return "Monkey";
                case 1:
                    return "Rooster";
                case 2:
                    return "Dog";
                case 3:
                    return "Pig";
                case 4:
                    return "Rat";
                case 5:
                    return "Ox";
                case 6:
                    return "Tiger";
                case 7:
                    return "Rabbit";
                case 8:
                    return "Dragon";
                case 9:
                    return "Snake";
                case 10:
                    return "Horse";
                case 11:
                    return "Goat";
            }
            return null;
        }

    }
}

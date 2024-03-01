using KMA.ProgrammingCSharp.HibskyiPractice1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.ProgrammingCSharp.HibskyiPractice1.ViewModels
{
    internal class CalculateAgeViewModel : INotifyPropertyChanged
    {
        private User _user = new();

        public DateTime DateOfBirth
        {
            get
            {
                return _user.DateOfBirth;
            }
            set
            {
                if(_user.DateOfBirth != value )
                {
                    try
                    {
                        _user.DateOfBirth = value;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }
                    OnPropertyChanged(nameof(DateOfBirth));
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(WesternZodiacSign));
                    OnPropertyChanged(nameof(ChineseZodiacSign));
                    if (_user.CheckBirthday()) 
                        MessageBox.Show("Happy Birthday!!!");
                }
            }
        }

        public int Age
        {
            get
            {
                return _user.Age;
            }
        }

        public string? WesternZodiacSign
        {
            get
            {
                return _user.WesternZodiacSign;
            }
        }

        public string? ChineseZodiacSign
        {
            get
            {
                return _user.ChineseZodiacSign;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

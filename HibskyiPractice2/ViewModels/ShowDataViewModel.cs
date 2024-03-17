using KMA.ProgrammingCSharp.HibskyiPractice2.Models;
using KMA.ProgrammingCSharp.HibskyiPractice2.Navigation;
using KMA.ProgrammingCSharp.HibskyiPractice2.Services;
using KMA.ProgrammingCSharp.HibskyiPractice2.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice2.ViewModels
{
    internal class ShowDataViewModel : INavigatable<MainNavigationTypes>
    {
        private RelayCommand<object> _goToEnterDataCommand;
        private Action _goToEnterData;

        public ShowDataViewModel(Action goToEnterData)
        {
            _goToEnterData = goToEnterData;
        }

        public string FirstName 
        { 
            get 
            { 
                return CurrentPersonService.CurrentPerson.FirstName; 
            }
        }

        public string LastName
        {
            get 
            {
                return CurrentPersonService.CurrentPerson.LastName; 
            }
        }

        public string Email
        {
            get 
            {
                if (CurrentPersonService.CurrentPerson.Email == null) 
                    return "";
                return CurrentPersonService.CurrentPerson.Email; 
            }
        }

        public string DateOfBirth
        {
            get
            {
                if (CurrentPersonService.CurrentPerson.DateOfBirth == null)
                    return "";
                return CurrentPersonService.CurrentPerson.DateOfBirth.Value.ToString("dd.MM.yyyy");
            }
        }

        public string IsAdult
        {
            get
            {
                if (CurrentPersonService.CurrentPerson.IsAdult == null)
                    return "";
                return CurrentPersonService.CurrentPerson.IsAdult.Value.ToString();
            }
        }

        public string SunSign
        {
            get
            {
                if (CurrentPersonService.CurrentPerson.SunSign == null)
                    return "";
                return CurrentPersonService.CurrentPerson.SunSign;
            }
        }

        public string ChineseSign
        {
            get
            {
                if (CurrentPersonService.CurrentPerson.ChineseSign == null)
                    return "";
                return CurrentPersonService.CurrentPerson.ChineseSign;
            }
        }

        public string IsBirthday
        {
            get
            {
                if (CurrentPersonService.CurrentPerson.IsBirthday == null)
                    return "";
                return CurrentPersonService.CurrentPerson.IsBirthday.Value.ToString();
            }
        }

        public RelayCommand<object> GoToEnterDataCommand
        {
            get
            {
                return _goToEnterDataCommand ??= new RelayCommand<object>(_ => _goToEnterData.Invoke());
            }
        }

        public MainNavigationTypes ViewType
        {
            get
            {
                return MainNavigationTypes.ShowData;
            }
        }
    }
}

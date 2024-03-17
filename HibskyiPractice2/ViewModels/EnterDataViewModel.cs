using KMA.ProgrammingCSharp.HibskyiPractice2.Managers;
using KMA.ProgrammingCSharp.HibskyiPractice2.Models;
using KMA.ProgrammingCSharp.HibskyiPractice2.Navigation;
using KMA.ProgrammingCSharp.HibskyiPractice2.Services;
using KMA.ProgrammingCSharp.HibskyiPractice2.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.ProgrammingCSharp.HibskyiPractice2.ViewModels
{
    internal class EnterDataViewModel : INavigatable<MainNavigationTypes>
    {
        private EnterPerson _enterPerson = new EnterPerson();
        private RelayCommand<object> _proceedCommand;
        private Action _goToShowData;

        public EnterDataViewModel(Action goToShowData)
        {
            _goToShowData = goToShowData;
        }

        public string FirstName
        {
            get
            {
                return _enterPerson.FirstName;
            }
            set
            {
                _enterPerson.FirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _enterPerson.LastName;
            }
            set
            {  
                _enterPerson.LastName = value;
            }
        }

        public string Email
        {
            get
            {
                return _enterPerson.Email;
            }
            set
            {
                _enterPerson.Email = value;
            }
        }

        public DateTime? DateOfBirth
        {
            get 
            {
                return _enterPerson.DateOfBirth;
            }
            set
            {
                _enterPerson.DateOfBirth = value;
            }
        }

        public RelayCommand<object> ProceedCommand
        {
            get
            {
                return _proceedCommand ??= new RelayCommand<object>(_ => Proceed(), CanExecute);
            }
        }

        public MainNavigationTypes ViewType
        {
            get
            {
                return MainNavigationTypes.EnterData;
            }
        }

        private async void Proceed()
        {
            if (String.IsNullOrWhiteSpace(_enterPerson.FirstName) || String.IsNullOrWhiteSpace(_enterPerson.LastName)
                || String.IsNullOrEmpty(_enterPerson.Email) || DateOfBirth == null)
            {
                MessageBox.Show("First name, Last name, Email or Date of birth is Empty");
                return;
            }

            var authService = new CurrentPersonService();

            try
            {
                LoaderManager.Instance.ShowLoader();
                await Task.Run(() => authService.EnterPerson(_enterPerson));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
            finally
            {
                LoaderManager.Instance.HideLoader();
            }

            _goToShowData.Invoke();
            if (CurrentPersonService.CurrentPerson.IsBirthday.Value)
                MessageBox.Show("Happy Birthday!!!");
        }

        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(_enterPerson.FirstName)
                && !String.IsNullOrWhiteSpace(_enterPerson.LastName)
                && !String.IsNullOrEmpty(_enterPerson.Email)
                && DateOfBirth != null;
        }

    }
}

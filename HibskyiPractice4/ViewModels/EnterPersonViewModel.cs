using KMA.ProgrammingCSharp.HibskyiPractice4.Navigation;
using KMA.ProgrammingCSharp.HibskyiPractice4.Models;
using KMA.ProgrammingCSharp.HibskyiPractice4.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KMA.ProgrammingCSharp.HibskyiPractice4.Services;
using KMA.ProgrammingCSharp.HibskyiPractice4.Managers;
using System.Collections.ObjectModel;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.ViewModels
{
    internal class EnterPersonViewModel : INavigatable<MainNavigationTypes>
    {
        private RelayCommand<object> _saveCommand;
        private RelayCommand<object> _cancelCommand;
        private Action _goToPersonList;

        public EnterPersonViewModel(Action goToPersonList)
        {
            _goToPersonList = goToPersonList;
        }

        public string FirstName
        {
            get
            {
                return ApplicationManager.CurrentEnterPerson.FirstName;
            }
            set
            {
                ApplicationManager.CurrentEnterPerson.FirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return ApplicationManager.CurrentEnterPerson.LastName;
            }
            set
            {
                ApplicationManager.CurrentEnterPerson.LastName = value;
            }
        }

        public string Email
        {
            get
            {
                return ApplicationManager.CurrentEnterPerson.Email;
            }
            set
            {
                ApplicationManager.CurrentEnterPerson.Email = value;
            }
        }

        public DateTime? DateOfBirth
        {
            get
            {   
                return ApplicationManager.CurrentEnterPerson.DateOfBirth;
            }
            set
            {
                ApplicationManager.CurrentEnterPerson.DateOfBirth = value;
            }
        }

        public RelayCommand<object> SaveCommand
        {
            get
            {
                return _saveCommand ??= new RelayCommand<object>(_ => Save(), CanExecute);
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(_ => _goToPersonList.Invoke());
            }
        }

        public MainNavigationTypes ViewType
        {
            get
            {
                return MainNavigationTypes.EnterPerson;
            }
        }

        private async void Save()
        {
            if (String.IsNullOrWhiteSpace(ApplicationManager.CurrentEnterPerson.FirstName) 
                || String.IsNullOrWhiteSpace(ApplicationManager.CurrentEnterPerson.LastName)
                || String.IsNullOrEmpty(ApplicationManager.CurrentEnterPerson.Email) 
                || DateOfBirth == null)
            {
                MessageBox.Show("First name, Last name, Email or Date of birth is Empty");
                return;
            }

            var personService = new PersonService();

            try
            {
                LoaderManager.Instance.ShowLoader();
                await Task.Run(() => personService.AddOrUpdatePerson(ApplicationManager.CurrentEnterPerson));
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

            new PersonListService().UpdatePersonList();
            _goToPersonList.Invoke();
        }

        private bool CanExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(ApplicationManager.CurrentEnterPerson.FirstName)
                && !String.IsNullOrWhiteSpace(ApplicationManager.CurrentEnterPerson.LastName)
                && !String.IsNullOrEmpty(ApplicationManager.CurrentEnterPerson.Email)
                && DateOfBirth != null;
        }

    }
}

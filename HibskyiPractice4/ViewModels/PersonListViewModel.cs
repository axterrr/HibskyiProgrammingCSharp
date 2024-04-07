using KMA.ProgrammingCSharp.HibskyiPractice4.Managers;
using KMA.ProgrammingCSharp.HibskyiPractice4.Models;
using KMA.ProgrammingCSharp.HibskyiPractice4.Navigation;
using KMA.ProgrammingCSharp.HibskyiPractice4.Services;
using KMA.ProgrammingCSharp.HibskyiPractice4.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.ViewModels
{
    internal class PersonListViewModel : INavigatable<MainNavigationTypes>, INotifyPropertyChanged
    {
        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _deletePersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _goToFiltrationCommand;
        private Action _goToEnterPerson;
        private Action _goToFiltration;
        private readonly PersonListService ListService = new();

        public PersonListViewModel(Action goToEnterPerson, Action goToFiltration)
        {
            ListService.UpdatePersonList();
            _goToEnterPerson = goToEnterPerson;
            _goToFiltration = goToFiltration;
        }

        public ObservableCollection<Person> PersonList
        {
            get 
            {
                return ListService.CurrentPersonList; 
            }
        }

        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return _addPersonCommand ??= new RelayCommand<object>(_ => 
                {
                    ApplicationManager.CurrentEnterPerson = new EnterPerson();
                    _goToEnterPerson.Invoke();
                });
            }
        }

        public RelayCommand<object> DeletePersonCommand
        {
            get
            {
                return _deletePersonCommand ??= new RelayCommand<object>(obj => DeletePerson(obj));
            }
        }

        public RelayCommand<object> EditPersonCommand
        {
            get
            {
                return _editPersonCommand ??= new RelayCommand<object>(obj => EditPerson(obj));
            }
        }

        public RelayCommand<object> GoToFiltrationCommand
        {
            get
            {
                return _goToFiltrationCommand ??= new RelayCommand<object>(obj => _goToFiltration.Invoke());
            }
        }

        private async void DeletePerson(object obj)
        {
            if(obj is not Person) 
            {
                return;
            }
            PersonService personService = new PersonService();
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() => personService.DeletePerson((Person)obj));
            LoaderManager.Instance.HideLoader();
            ListService.UpdatePersonList();
            OnPropertyChanged(nameof(PersonList));
        }

        private void EditPerson(object obj)
        {
            if (obj is not Person)
            {
                return;
            }
            Person p = (Person)obj;
            ApplicationManager.CurrentEnterPerson = new EnterPerson(p.Guid, p.FirstName, p.LastName, p.Email, p.DateOfBirth);
            _goToEnterPerson.Invoke();
        }

        public MainNavigationTypes ViewType
        {
            get
            {
                return MainNavigationTypes.PersonList;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

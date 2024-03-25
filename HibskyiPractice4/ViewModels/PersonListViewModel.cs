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
        private Action _goToEnterPerson;

        public PersonListViewModel(Action goToEnterPerson)
        {
            _goToEnterPerson = goToEnterPerson;
            ApplicationManager.CurrentPersonList = new ObservableCollection<Person>(new PersonService().GetAllPersons()); 
        }

        public ObservableCollection<Person> PersonList
        {
            get 
            {
                return ApplicationManager.CurrentPersonList; 
            }
            set
            {
                ApplicationManager.CurrentPersonList = value;
                OnPropertyChanged();
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

        private void DeletePerson(object obj)
        {
            if(obj is not Person) 
            {
                return;
            }
            PersonService personService = new PersonService();
            personService.DeletePerson((Person)obj);
            PersonList = new ObservableCollection<Person>(personService.GetAllPersons());
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

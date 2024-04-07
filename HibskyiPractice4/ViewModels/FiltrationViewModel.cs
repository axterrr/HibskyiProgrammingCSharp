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
using System.Windows.Controls;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.ViewModels
{
    internal class FiltrationViewModel : INavigatable<MainNavigationTypes>, INotifyPropertyChanged
    {
        private Filter _filter = new();
        private RelayCommand<object> _applyFilterCommand;
        private RelayCommand<object> _resetFilterCommand;
        private Action _goToPersonList;

        public FiltrationViewModel(Action goToPersonList)
        {
            _goToPersonList = goToPersonList;
        }

        public string FirstName
        {
            get => _filter.FirstName;
            set => _filter.FirstName = value;
        }

        public string LastName
        {
            get => _filter.LastName;
            set => _filter.LastName = value;
        }

        public string Email
        {
            get => _filter.Email;
            set => _filter.Email = value;
        }

        public DateTime? DateOfBirthFrom
        {
            get => _filter.DateOfBirthFrom;
            set => _filter.DateOfBirthFrom = value;
        }

        public DateTime? DateOfBirthTo
        {
            get => _filter.DateOfBirthTo;
            set => _filter.DateOfBirthTo = value;
        }

        public bool? IsAdult
        {
            get => _filter.IsAdult;
            set => _filter.IsAdult = value;
        }
        
        public string SunSign
        {
            get => _filter.SunSign;
            set => _filter.SunSign = value;
        }

        public string ChineseSign
        {
            get => _filter.ChineseSign;
            set => _filter.ChineseSign = value;
        }

        public bool? IsBirthday
        {
            get => _filter.IsBirthday;
            set => _filter.IsBirthday = value;
        }

        public int SortedBy
        {
            get => Convert.ToInt32(_filter.SortedBy);
            set => _filter.SortedBy = (Filter.SortingBy)Enum.Parse(typeof(Filter.SortingBy), value.ToString());
        }

        public bool DescendingOrder
        {
            get => _filter.DescendingOrder;
            set => _filter.DescendingOrder = value;
        }

        public RelayCommand<object> ApplyFilterCommand
        {
            get
            {
                return _applyFilterCommand ??= new RelayCommand<object>(_ => ApplyFilter());
            }
        }

        public RelayCommand<object> ResetFilterCommand => _resetFilterCommand ??= new RelayCommand<object>(_ => 
        {
            FirstName = null;
            LastName = null;
            Email = null;
            DateOfBirthFrom = null;
            DateOfBirthTo = null;
            IsAdult = null;
            SunSign = null;
            ChineseSign = null;
            IsBirthday = null;
            _filter.SortedBy = Filter.SortingBy.FirstName;
            DescendingOrder = false;

            OnPropertyChanged(nameof(FirstName));
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Email));
            OnPropertyChanged(nameof(DateOfBirthFrom));
            OnPropertyChanged(nameof(DateOfBirthTo));
            OnPropertyChanged(nameof(IsAdult));
            OnPropertyChanged(nameof(SunSign));
            OnPropertyChanged(nameof(ChineseSign));
            OnPropertyChanged(nameof(IsBirthday));
            OnPropertyChanged(nameof(SortedBy));
            OnPropertyChanged(nameof(DescendingOrder));
        });

        private void ApplyFilter()
        {
            PersonListService listService = new();
            listService.CurrentFilter = new Filter(FirstName, LastName, Email, DateOfBirthFrom, DateOfBirthTo,
                IsAdult, SunSign, ChineseSign, IsBirthday, _filter.SortedBy, DescendingOrder);
            listService.UpdatePersonList();
            _goToPersonList.Invoke();
        }

        public MainNavigationTypes ViewType
        {
            get
            {
                return MainNavigationTypes.Filtration;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

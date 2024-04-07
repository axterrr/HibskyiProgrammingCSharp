using KMA.ProgrammingCSharp.HibskyiPractice4.Managers;
using KMA.ProgrammingCSharp.HibskyiPractice4.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Services
{
    internal class PersonListService
    {
        private static ObservableCollection<Person> _currentPersonList;
        private static Filter _currentFilter = new();

        public void UpdatePersonList()
        {
            List<Person> list = new PersonService().GetAllPersons();

            var filteredList = list
                .Where(p => (String.IsNullOrEmpty(CurrentFilter.FirstName) || p.FirstName.ToLower().Contains(CurrentFilter.FirstName.ToLower()))
                        && (String.IsNullOrEmpty(CurrentFilter.LastName) || p.LastName.ToLower().Contains(CurrentFilter.LastName.ToLower()))
                        && (String.IsNullOrEmpty(CurrentFilter.Email) || p.Email.ToLower().Contains(CurrentFilter.Email.ToLower()))
                        && (String.IsNullOrEmpty(CurrentFilter.SunSign) || p.SunSign.ToString().ToLower().Contains(CurrentFilter.SunSign.ToLower()))
                        && (String.IsNullOrEmpty(CurrentFilter.ChineseSign) || p.ChineseSign.ToString().ToLower().Contains(CurrentFilter.ChineseSign.ToLower()))
                        && (CurrentFilter.IsAdult == null || (p.IsAdult == CurrentFilter.IsAdult))
                        && (CurrentFilter.IsBirthday == null || (p.IsBirthday == CurrentFilter.IsBirthday))
                        && (CurrentFilter.DateOfBirthFrom == null || (p.DateOfBirth >= CurrentFilter.DateOfBirthFrom))
                        && (CurrentFilter.DateOfBirthTo == null || (p.DateOfBirth <= CurrentFilter.DateOfBirthTo)));

            var sortedList = (CurrentFilter.SortedBy) switch
            {
                Filter.SortingBy.FirstName => filteredList.OrderBy(p => p.FirstName),
                Filter.SortingBy.LastName => filteredList.OrderBy(p => p.LastName),
                Filter.SortingBy.Email => filteredList.OrderBy(p => p.Email),
                Filter.SortingBy.DateOfBirth => filteredList.OrderBy(p => p.DateOfBirth),
                Filter.SortingBy.IsAdult => filteredList.OrderBy(p => p.IsAdult),
                Filter.SortingBy.SunSign => filteredList.OrderBy(p => Convert.ToInt32(p.SunSign)),
                Filter.SortingBy.ChineseSign => filteredList.OrderBy(p => Convert.ToInt32(p.ChineseSign)),
                Filter.SortingBy.IsBirthday => filteredList.OrderBy(p => p.IsBirthday),
                _ => filteredList.OrderBy(p => p.FirstName)
            };

            var finalList = CurrentFilter.DescendingOrder ? sortedList.Reverse() : sortedList;

            CurrentPersonList = new ObservableCollection<Person>(finalList);
        }

        public ObservableCollection<Person> CurrentPersonList
        {
            get { return _currentPersonList; }
            private set { _currentPersonList = value; }
        }

        public Filter CurrentFilter
        {
            get { return _currentFilter; }
            set { _currentFilter = value; }
        }

    }
}

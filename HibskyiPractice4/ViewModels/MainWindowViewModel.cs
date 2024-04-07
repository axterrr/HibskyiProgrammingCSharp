using KMA.ProgrammingCSharp.HibskyiPractice4.Managers;
using KMA.ProgrammingCSharp.HibskyiPractice4.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.ViewModels
{
    internal class MainWindowViewModel : BaseNavigatableViewModel<MainNavigationTypes>, ILoaderOwner
    {
        private bool _isEnabled = true;
        private Visibility _loaderVisibility = Visibility.Collapsed;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public Visibility LoaderVisibility
        {
            get { return _loaderVisibility; }
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            LoaderManager.Instance.Initialize(this);
            Navigate(MainNavigationTypes.PersonList);
        }

        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            switch (type)
            {
                case MainNavigationTypes.EnterPerson:
                    return new EnterPersonViewModel(() => Navigate(MainNavigationTypes.PersonList));
                case MainNavigationTypes.Filtration:
                    return new FiltrationViewModel(() => Navigate(MainNavigationTypes.PersonList));
                case MainNavigationTypes.PersonList:
                    return new PersonListViewModel(() => Navigate(MainNavigationTypes.EnterPerson), () => Navigate(MainNavigationTypes.Filtration));
                default:
                    return null;
            }
        }
    }
}

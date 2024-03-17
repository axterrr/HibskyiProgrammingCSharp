using KMA.ProgrammingCSharp.HibskyiPractice2.Managers;
using KMA.ProgrammingCSharp.HibskyiPractice2.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.ProgrammingCSharp.HibskyiPractice2.ViewModels
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
            Navigate(MainNavigationTypes.EnterData);
        }

        protected override INavigatable<MainNavigationTypes> CreateViewModel(MainNavigationTypes type)
        {
            switch (type)
            {
                case MainNavigationTypes.EnterData:
                    return new EnterDataViewModel(() => Navigate(MainNavigationTypes.ShowData));
                case MainNavigationTypes.ShowData:
                    return new ShowDataViewModel(() => Navigate(MainNavigationTypes.EnterData));
                default:
                    return null;
            }
        }
    }
}

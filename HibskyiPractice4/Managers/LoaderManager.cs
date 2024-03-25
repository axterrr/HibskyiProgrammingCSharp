using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KMA.ProgrammingCSharp.HibskyiPractice4.Managers
{
    internal class LoaderManager
    {
        private static readonly object Locker = new object();
        private static LoaderManager _instance;
        private ILoaderOwner _loaderOwner;

        public static LoaderManager Instance
        {
            get 
            { 
                if(_instance != null)
                    return _instance;
                lock (Locker) 
                {
                    return _instance ??= new LoaderManager();
                }
            }
        }

        private LoaderManager() {}

        public void Initialize(ILoaderOwner loaderOwner)
        {
            _loaderOwner = loaderOwner;
        }

        public void ShowLoader()
        {
            _loaderOwner.IsEnabled = false;
            _loaderOwner.LoaderVisibility = Visibility.Visible;
        }

        public void HideLoader()
        {
            _loaderOwner.IsEnabled = true;
            _loaderOwner.LoaderVisibility = Visibility.Collapsed;
        }

    }
}

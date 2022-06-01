using Invi_2._0.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Invi_2._0.ViewModels
{
     class AppNavigationViewModel : INotifyPropertyChanged
    {
        private Page MainDevicePage;
        private Page SettingsPage;
        private Page AllDevicePage;
        private Page _currentPage;
        public Page CurrentPage
        {
            get
            {
                return _currentPage;

            }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        UserModel userModel = new UserModel();


        public AppNavigationViewModel()
        {
            MainDevicePage = new Views.MainDevicePage();
            AllDevicePage = new Views.AllDevicePage();
            SettingsPage = new Views.SettingsPage();
            CurrentPage = MainDevicePage;
        }

        public ICommand LoadAllDevicePage
        {
            get
            {
                return new DelegateСommand(async (obj) =>
                {
                    CurrentPage = AllDevicePage;
                });
            }
        }

        public ICommand LoadAllMainDevicePage
        {
            get
            {
                return new DelegateСommand(async (obj) =>
                {
                    CurrentPage = MainDevicePage;
                });
            }
        }

        public ICommand LoadSettings
        {
            get
            {
                return new DelegateСommand(async (obj) =>
                {
                    CurrentPage = SettingsPage;
                });
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
     }

}


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
        private Page DevicesPage;
       
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
            DevicesPage = new Views.DevisesPage();
            CurrentPage = DevicesPage;
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


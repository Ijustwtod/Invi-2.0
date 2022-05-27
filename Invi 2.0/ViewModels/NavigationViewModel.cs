using Invi_2._0.Models;
using Invi_2._0.Models.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace Invi_2._0.ViewModels
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        private Page DevicesPage;
        private Page StartPage;

        private Page _currentPage;
        public Page CurrentPage
        {
            get
            {
                return _currentPage;
                OnPropertyChanged("CurrentPage");
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        UserModel userModel = new UserModel();
      

        public NavigationViewModel()
        {
            DevicesPage = new Views.DevisesPage();
            StartPage = new Views.StartPage();

            if (User.qauthtoken!="")
            {
                CurrentPage = DevicesPage;
            }
            else
            {
                CurrentPage = StartPage;
            }
        }
      
        public ICommand UnLogin
        {
            get
            {
                return new DelegateСommand((obj) =>
                {
                    CurrentPage = StartPage;
                    userModel.DeleteUser();
                    OnPropertyChanged("CurrentPage");
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

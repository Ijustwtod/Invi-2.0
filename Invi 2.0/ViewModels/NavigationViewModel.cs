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
        private Page AppMainWindow;
        private Page AuthPage;

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
      
        public NavigationViewModel()
        {
            AppMainWindow = new Views.AppMainWindow();
            AuthPage = new Views.AuthPage();

            UserModel user = new UserModel();

            if (user.SearchUser())
            {
                CurrentPage = AppMainWindow;
            }
            else
            {
                CurrentPage = AuthPage;
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

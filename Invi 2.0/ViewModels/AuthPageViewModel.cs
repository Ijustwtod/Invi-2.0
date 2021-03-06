using Invi_2._0.Models;
using Invi_2._0.Models.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Invi_2._0.ViewModels
{
    class AuthPageViewModel : INotifyPropertyChanged
    {
        private UserModel UserModel;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private string _code;
        public string Code
        {
            get { return _code; }
            set { _code = value; OnPropertyChanged(nameof(Code)); }
        }

        WebBrowserWindow webBrowserWindow = new WebBrowserWindow();

        public ICommand Login
        {
            get
            {
                return new DelegateСommand((obj) =>
                {
                    webBrowserWindow.ShowDialog();
                    if (User.qauthtoken != " ")
                    {
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                });
            }
        }

        public AuthPageViewModel()
        {
            UserModel = new UserModel();
        }

        public ICommand Save
        {
            get
            {
                return new DelegateСommand((obj) =>
                {
                    if (Code != "")
                    {
                        UserModel.CreateUser(Code);
                    }
                });
            }
        }
    }
}

using Invi_2._0.Models;
using Invi_2._0.Models.Data;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Invi_2._0.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        UserModel userModel = new UserModel();
        public ICommand UnLogin
        {
            get
            {
                return new DelegateСommand((obj) =>
                {
                    if (User.qauthtoken != " ")
                    {
                        userModel.DeleteUser();
                        System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                        Application.Current.Shutdown();
                    }
                });
            }
        }


    }
}

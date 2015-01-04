using GalaSoft.MvvmLight.CommandWpf;
using nmct.ba.cashlessproject.model.IT_Bedrijf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.Management.ViewModel
{
    class WachtwoordWijzigenVM : ObservableObject, IPage
    {
        private static WebAccess wa = new WebAccess();

        #region PROPERTIES
        public string Name
        {
            get { return "WACHTWOORD WIJZIGEN"; }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("UserName"); }
        }

        private string _newPass;

        public string NewPass
        {
            get { return _newPass; }
            set { _newPass = value; OnPropertyChanged("NewPass"); }
        }

        private string _repeatNewPass;

        public string RepeatNewPass
        {
            get { return _repeatNewPass; }
            set { _repeatNewPass = value; OnPropertyChanged("RepeatNewPass"); }
        }

        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        #endregion

        public ICommand ChangePasswordCommand
        {
            get { return new RelayCommand(ChangePassword); }
        }
        private async void ChangePassword()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;

            if (ApplicationVM.token != null && !ApplicationVM.token.IsError)
            {
                
                if (Username != null && Username != "" && NewPass != null && NewPass != "" && RepeatNewPass != null && RepeatNewPass != "")
                {
                    NewPassword np = new NewPassword();
                    np.Username = Username;
                    np.NewPass = NewPass;
                    np.RepeatNewPass = RepeatNewPass;

                    if (np.NewPass == np.RepeatNewPass)
                    {
                        await wa.UpdatePassword(np, ApplicationVM.token.AccessToken);
                        appvm.ChangePage(new ProductenMV());
                    }
                    else
                    {
                        Error = "De ingegeven wachtwoorden komen niet overeen";
                    }
                }
                else
                {
                    Error = "Gelieve alle velden in te vullen!";
                }
            }
            else
            {
                App.Current.MainWindow.Close();
            }
        }   
    }
}

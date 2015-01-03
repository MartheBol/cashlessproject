using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Management.ViewModel
{
    class LoginVM : ObservableObject,IPage
    {
        private static WebAccess webserviceAccess = new WebAccess();

        public string Name
        {
            get { return "LOGIN"; }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged("Username"); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged("Password"); }
        }

        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        private void Login()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            ApplicationVM.token = WebAccess.GetToken(Username, Password);

            if (!ApplicationVM.token.IsError)
            {
                appvm.ChangePage(new ProductenMV());
            }
            else
            {
                Error = "Gebruikersnaam of wachtwoord kloppen niet";
            }
        }
    }
}

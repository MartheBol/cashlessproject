using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Klant.ViewModel
{
    class StartSchermVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "StartScherm"; }        
        }

        public ICommand RegistrerenCommand
        {
            get { return new RelayCommand(Registreren); }
        }

        private void Registreren()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new RegisterGegevensVM());
        }

        public ICommand AanmeldenCommand
        {
            get { return new RelayCommand(Aanmelden); }
        }

        private void Aanmelden()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new AanmeldenVM());
        }
    }
}

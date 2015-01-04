using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Management.ViewModel
{
    class StatistiekTotaleVerkoopVM : ObservableObject,IPage
    {
        public string Name
        {
            get { return "Statistiek Totale verkoop"; }
        }

        #region Afmelden
        private void Afmelden()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            ApplicationVM.token = null;

            if (ApplicationVM.token == null)
            {
                appvm.ChangePage(new LoginVM());
            }
        }

        public ICommand AfmeldenCommand
        {
            get { return new RelayCommand(Afmelden); }
        }
        #endregion
    }
}

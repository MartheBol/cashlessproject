using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.Management.ViewModel
{
    class KassaVM : ObservableObject,IPage
    {
        public string Name
        {
            get { return "Kassas"; }
        }

        public KassaVM()
        {
            if (ApplicationVM.token != null)
            {
                GetKassas();
            }
        }

        private ObservableCollection<Registers> _register;
        public ObservableCollection<Registers> Registers
        {
            get { return _register; }
            set { _register = value; OnPropertyChanged("Registers"); }
        }

        private Registers _selectedRegister;
        public Registers SelectedRegister
        {
            get { return _selectedRegister; }
            set { _selectedRegister = value; OnPropertyChanged("SelectedRegister"); }
        }


        private async void GetKassas()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync("http://localhost:13160/api/Registers/");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Registers = JsonConvert.DeserializeObject<ObservableCollection<Registers>>(json);
                }
            }
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

        public ICommand WachtwoordWijzigenCommand
        {
            get { return new RelayCommand(WachtwoordWijzigen); }
        }
        private void WachtwoordWijzigen()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            TokenResponse token = ApplicationVM.token;
            if (!token.IsError)
            {
                appvm.ChangePage(new WachtwoordWijzigenVM());
            }
            else
            {
                App.Current.MainWindow.Close();
            }
        }



    }
}

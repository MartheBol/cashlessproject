using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Management.ViewModel
{
    class KlantenVM : ObservableObject, IPage
    {
        #region PROPERTIES
        public string Name
        {
            get { return "Klanten"; }
        }

        public KlantenVM()
        {
            if (ApplicationVM.token != null)
            {
                GetKlanten();
            }
            
        }

        private ObservableCollection<Customers> _klanten;

        public ObservableCollection<Customers> Klanten
        {
            get { return _klanten; }
            set { _klanten = value; OnPropertyChanged("Klanten"); }

        }

        private Customers _selectedKlant;
        public Customers SelectedKlant
        {
            get { return _selectedKlant; }
            set { _selectedKlant = value; OnPropertyChanged("SelectedKlant"); }
        }

        #endregion 

        #region METHODS
        private async void GetKlanten()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customers/");
                if(response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Klanten = JsonConvert.DeserializeObject<ObservableCollection<Customers>>(json);
                    

                }
            }
        }

        public async void SaveKlanten()
        {
           
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string Klant = JsonConvert.SerializeObject(SelectedKlant);
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customers/" + SelectedKlant.Id, new StringContent(Klant, Encoding.UTF8, "application/json"));
                GetKlanten();
            }
        }

        private void AddImage()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                SelectedKlant.Picture = GetPhoto(ofd.FileName);
                OnPropertyChanged("SelectedCustomer");
            }
        }

        private byte[] GetPhoto(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            fs.Close();

            return data;
        }


        #endregion

        #region COMMANDS
        public ICommand SaveKlantCommand
        {
            get { return new RelayCommand(SaveKlanten); }
        }

        public ICommand AddImageCommand
        {
            get { return new RelayCommand(AddImage); }
        }
        #endregion


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

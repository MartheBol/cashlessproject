using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Klant.ViewModel
{
    class OpladenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Opladen"; }
        }

        private Customers _customer;

        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }
        private string _opladen;

        public string Opladen
        {
            get { return _opladen; }
            set { _opladen = value; OnPropertyChanged("Opladen"); }
        }
        private string _error;

        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }
        private double _totaalBedrag;

        public double TotaalBedrag
        {
            get { return _totaalBedrag; }
            set { _totaalBedrag = value; OnPropertyChanged("TotaalBedrag"); }
        }

        private void Controle()
        {
            int bedrag = Convert.ToInt32(Opladen);
            TotaalBedrag = SelectedCustomer.Balance + bedrag;
            if (TotaalBedrag > 100)
            {
                MakeErrorLog("Het saldo voor de klant : " + SelectedCustomer.CustomerName + "kon niet worden opgeladen.", "OpladenVM", "Controleren");
                MessageBox.Show("Uw kaart heeft het maximum bedrag van 100 euro overschreden, gelieve een kleiner bedrag op te laden");
            }
            else
            {
                UpdateKlant();
            }
        }

        public ICommand OpladenCommand
        {
            get { return new RelayCommand(Controle); }
        }

        public OpladenVM()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            if (appvm.ActiveUserId > 0)
            {
                GetKlant(appvm.ActiveUserId);

            }
        }

        private async void GetKlant(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:13160/api/Customers/" + id);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    SelectedCustomer = JsonConvert.DeserializeObject<Customers>(json);
                }
            }
        }

        private async void UpdateKlant()
        {
            using (HttpClient client = new HttpClient())
            {
                Customers kl = SelectedCustomer;
                kl.Balance = TotaalBedrag;
                string input = JsonConvert.SerializeObject(kl);
                HttpResponseMessage response = await client.PutAsync("http://localhost:13160/api/klant/", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.ChangePage(new OpladenVM());
                }
            }
        }

        public ICommand AnnulerenCommand
        {
            get { return new RelayCommand(Annuleren); }
        }

        private void Annuleren()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new StartSchermVM());
        }

        private async void MakeErrorLog(string message, string stacktraceClass, string stacktraceMethod)
        {

            Errorlog errorLog = new Errorlog();
            errorLog.RegisterId = Properties.Settings.Default.RegisterID;
            errorLog.Timestamp = DateTime.Now;
            errorLog.Message = message;
            errorLog.StackTrace = "Class: " + stacktraceClass + " Method: " + stacktraceMethod;

            string input = JsonConvert.SerializeObject(errorLog);
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync("http://localhost:13160/api/ErrorlogKlanten", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.ChangePage(new OpladenVM());
                }
            }
        }
    }
}
    
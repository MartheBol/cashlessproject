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

namespace nmct.ba.cashlessproject.ui.Management.ViewModel
{
    class KlantenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Klanten"; }
        }

        public KlantenVM()
        {
            GetKlanten();
        }

        private ObservableCollection<Customers> _klanten;

        public ObservableCollection<Customers> Klanten
        {
            get { return _klanten; }
            set { _klanten = value; OnPropertyChanged("Medewerkers"); }
        }

        private async void GetKlanten()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/customers/");
                if(response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Klanten = JsonConvert.DeserializeObject<ObservableCollection<Customers>>(json);

                }
            }
        }


        private Customers _selectedKlant;
        public Customers SelectedKlant
        {
            get { return _selectedKlant; }
            set { _selectedKlant = value; OnPropertyChanged("SelectedKlant"); }
        }
    }
}

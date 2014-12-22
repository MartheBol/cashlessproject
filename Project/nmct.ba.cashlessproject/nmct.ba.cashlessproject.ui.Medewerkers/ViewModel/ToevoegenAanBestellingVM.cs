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

namespace nmct.ba.cashlessproject.ui.Medewerkers.ViewModel
{
    class ToevoegenAanBestellingVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "ToevoegenAanBestelling"; }
        }

        public ToevoegenAanBestellingVM()
        {
            GetProducten();
        }

        private ObservableCollection<Products> _producten;
        public ObservableCollection<Products> Producten
        {
            get { return _producten; }
            set { _producten = value; OnPropertyChanged("Producten"); }
        }

        private async void GetProducten()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ConfigurationSettings.AppSettings.Get("apiUrl") + "api/products");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Producten = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                }
            }
        }


    }
}

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
    class StatistiekKassasVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Statistiek kassa"; }
        }

        public StatistiekKassasVM()
        {
            GetRegisters();
        }


        private ObservableCollection<Registers> _kassas;
        public ObservableCollection<Registers> Kassas
        {
            get { return _kassas; }
            set { _kassas = value; OnPropertyChanged("Kassas"); }
        }

        private Registers _selectedKassa;
        public Registers SelectedKassa
        {
            get { return _selectedKassa; }
            set { _selectedKassa = value; OnPropertyChanged("SelectedKassa"); }
        }

        private async void GetRegisters()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/register/");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Kassas = JsonConvert.DeserializeObject<ObservableCollection<Registers>>(json);
                }
            }
        }

    }


}

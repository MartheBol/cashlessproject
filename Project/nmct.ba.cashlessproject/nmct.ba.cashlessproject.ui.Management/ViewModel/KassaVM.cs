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
    class KassaVM : ObservableObject,IPage
    {
        public string Name
        {
            get { return "Kassas"; }
        }

        public KassaVM()
        {
            GetRegisters();
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

        private async void GetRegisters()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/register/");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Registers = JsonConvert.DeserializeObject<ObservableCollection<Registers>>(json);
                }
            }
        }


    }
}

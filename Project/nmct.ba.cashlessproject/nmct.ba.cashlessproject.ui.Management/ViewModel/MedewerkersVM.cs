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

namespace nmct.ba.cashlessproject.ui.Management.ViewModel
{
    class MedewerkersVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Medewerkers"; }
        }

        public MedewerkersVM()
        {
            GetMedewerkers();
        }

        private ObservableCollection<Employee> _medewerkers;

        public ObservableCollection<Employee> Medewerkers
        {
            get { return _medewerkers; }
            set { _medewerkers = value; OnPropertyChanged("Medewerkers"); }
        }

        private async void GetMedewerkers()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ConfigurationSettings.AppSettings.Get("apiUrl")+"api/employees");
                if(response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Medewerkers = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);

                }
            }
        }


        public async void DeleteMedewerker()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employees/" + SelectedMedewerker.Id);

                if (response.IsSuccessStatusCode)
                {
                    Medewerkers.Remove(SelectedMedewerker);
                    

                }
            }
        }


        public ICommand DeleteMederwerkerCommand
        {
            get { return new RelayCommand(DeleteMedewerker); }
        }


        private Employee _selectedMedewerker;
        public Employee SelectedMedewerker
        {
            get { return _selectedMedewerker; }
            set { _selectedMedewerker = value; OnPropertyChanged("SelectedMedewerker"); }
        }
       

    }
}

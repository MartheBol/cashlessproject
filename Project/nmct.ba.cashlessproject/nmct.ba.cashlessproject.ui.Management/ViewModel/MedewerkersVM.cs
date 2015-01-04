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
        #region Properties
        public string Name
        {
            get { return "Medewerkers"; }
        }

        public MedewerkersVM()
        {
            if (ApplicationVM.token != null)
            {
                GetMedewerkers();
            }
        }

        private ObservableCollection<Employee> _medewerkers;

        public ObservableCollection<Employee> Medewerkers
        {
            get { return _medewerkers; }
            set { _medewerkers = value; OnPropertyChanged("Medewerkers"); }
        }

        private Employee _selectedMedewerker;
        public Employee SelectedMedewerker
        {
            get { return _selectedMedewerker; }
            set { _selectedMedewerker = value; OnPropertyChanged("SelectedMedewerker"); }
        }
        #endregion

        #region Methods

        private async void GetMedewerkers()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employees");
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
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employees" + SelectedMedewerker.Id);

                if (response.IsSuccessStatusCode)
                {
                    Medewerkers.Remove(SelectedMedewerker);
                    

                }
            }
        }

       


        public async void AddEmployee()
        {
            Employee newEmployee = new Employee() {EmployeeName = "Nieuwe mederwerker", Address="Nieuw", Email="nieuw@nieuw.com", Phone=1234};
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string employee = JsonConvert.SerializeObject(newEmployee);
                HttpResponseMessage response = await client.PostAsync("http://localhost:13160/api/employees/", new StringContent(employee, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Employee emp = JsonConvert.DeserializeObject<Employee>(json);
                    if (emp != null)
                    {
                        Medewerkers.Add(emp);
                        SelectedMedewerker = emp;
                    }

                }
            }
        } 
        
        public async void SaveEmployee()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string Employee = JsonConvert.SerializeObject(SelectedMedewerker);
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/employees/" + SelectedMedewerker.Id, new StringContent(Employee, Encoding.UTF8, "application/json"));
            }
        }


        #endregion

        #region Commands
        public ICommand DeleteMederwerkerCommand
        {
            get { return new RelayCommand(DeleteMedewerker); }
        }
        public ICommand AddEmployeeCommand
        {
            get { return new RelayCommand(AddEmployee); }
        }
        public ICommand SaveEmployeeCommand
        {
            get { return new RelayCommand(SaveEmployee); }
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

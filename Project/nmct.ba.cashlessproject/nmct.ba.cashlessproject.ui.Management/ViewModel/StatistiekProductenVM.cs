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
    class StatistiekProductenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Statistiek producten"; }
        }

        public StatistiekProductenVM(){
            GetProducten();
        }

        private ObservableCollection<Products> _product;
        public ObservableCollection<Products> Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged("Producten"); }
        }

        private async void GetProducten()
        {
           using (HttpClient client = new HttpClient())
           {

               HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] +"api/products");
               if(response.IsSuccessStatusCode)
               {
                   string json = await response.Content.ReadAsStringAsync();
                   Product = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
               }
           }
        }

        private Products _selectedProduct;
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }



    }

}

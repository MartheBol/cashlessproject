using System;
using nmct.ba.cashlessproject.model.Klanten;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;

namespace nmct.ba.cashlessproject.ui.ViewModel
{
    class ManagementVM: ObservableObject, IPage
    {
        public string Name
        {
            get { return "Management"; }
        }
        public ManagementVM()
        {
            GetProduten();
        }

        private ObservableCollection<Products> _products;

        public ObservableCollection<Products> Products
        {
            get { return _products; }
            set {_products = value; OnPropertyChanged("Products");}
        }

        private async void GetProduten()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(ConfigurationSettings.AppSettings.Get("apiUrl")+"api/products");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Products = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
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

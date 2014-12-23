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
    class ProductenMV : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Producten"; }
        }


        public ProductenMV()
        {
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
                HttpResponseMessage response = await client.GetAsync(ConfigurationSettings.AppSettings.Get("apiUrl") + "api/products");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Product = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                }
            }
        }

        public async void DeleteProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/products/" + SelectedProduct.Id);

                if (response.IsSuccessStatusCode)
                {
                    Product.Remove(SelectedProduct);

                }
            }
        }


        public async void SaveProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                string Product = JsonConvert.SerializeObject(SelectedProduct);
                HttpResponseMessage response = await
                client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/products/" + SelectedProduct.Id, new StringContent(Product, Encoding.UTF8, "application/json"));
            }
        }







        private Products _selectedProduct;
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }

        public ICommand DeleteProductCommand
        {
            get { return new RelayCommand(DeleteProduct); }
        }

        public ICommand SaveProductCommand
        {
            get { return new RelayCommand(SaveProduct); }
        }



     
   



    }
}

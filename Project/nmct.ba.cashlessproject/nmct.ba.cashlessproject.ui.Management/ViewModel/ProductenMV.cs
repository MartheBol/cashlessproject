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
        #region Properties
        public string Name
        {
            get { return "Producten"; }
        }


        public ProductenMV()
        {
            if (ApplicationVM.token != null)
            {
                GetProducten();
            }
        }

        private ObservableCollection<Products> _product;
        public ObservableCollection<Products> Product
        {
            get { return _product; }
            set { _product = value; OnPropertyChanged("Product"); }
        }
        private Products _selectedProduct;
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }

        #endregion

        #region Methods
        private async void GetProducten()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/products/");
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
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.DeleteAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/products/" + SelectedProduct.Id);

                if (response.IsSuccessStatusCode)
                {
                    Product.Remove(SelectedProduct);

                }
            }
        }


       private async void AddProduct()
        {
            Products newProduct = new Products() { ProductName = "Nieuw Product" };
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string emp = JsonConvert.SerializeObject(newProduct);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/products", new StringContent(emp, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    GetProducten();
                }
            }
        }

    
        public async void SaveProduct()
        {
            using (HttpClient client = new HttpClient())
            {
                client.SetBearerToken(ApplicationVM.token.AccessToken);
                string Employee = JsonConvert.SerializeObject(SelectedProduct);
                HttpResponseMessage response = await client.PutAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/products/" + SelectedProduct.Id , new StringContent(Employee, Encoding.UTF8, "application/json"));
            }
        }

        #endregion

        #region Commands
       public ICommand DeleteProductCommand
        {
            get { return new RelayCommand(DeleteProduct); }
        }

        public ICommand AddProductCommand
        {
            get { return new RelayCommand(AddProduct); }
        }

        public ICommand SaveProductCommand
        {
            get { return new RelayCommand(SaveProduct); }
        }

        public ICommand RefreshProductsCommand
        {
            get { return new RelayCommand(GetProducten); }
        }
    #endregion

    }
}

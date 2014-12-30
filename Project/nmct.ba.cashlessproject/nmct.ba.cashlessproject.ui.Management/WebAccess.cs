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
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.Management
{
    public class WebAccess
    {
        private static string URL = ConfigurationManager.AppSettings["apiUrl"];

        public static TokenResponse GetToken(string username, string password)
        {
            var client = new OAuth2Client(new Uri(URL + "token"));
            return client.RequestResourceOwnerPasswordAsync(username, password).Result;
        }


        #region Registers

        public async Task<ObservableCollection<Registers>> GetRegisters(string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            HttpResponseMessage response = await client.GetAsync(URL+ "api/register");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                ObservableCollection<Registers> registers = JsonConvert.DeserializeObject<ObservableCollection<Registers>>(json);
                return registers;
            }
            return null;
        }

        #endregion

        #region Customers

        public async Task<ObservableCollection<Customers>> GetCustomers(string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            HttpResponseMessage response = await client.GetAsync(URL + "api/customers");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                ObservableCollection<Customers> customers = JsonConvert.DeserializeObject<ObservableCollection<Customers>>(json);
                return customers;
            }
            return null;
        }
        public async Task UpdateCustomer(Customers c, Customers selectedC, string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            c = selectedC;
            string input = JsonConvert.SerializeObject(c);
            HttpResponseMessage response = await client.PutAsync(URL + "api/customers", new StringContent(input, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await GetCustomers();
            }
        }

        #endregion

        #region Employees

        public async Task<ObservableCollection<Employee>> GetEmployees(string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            HttpResponseMessage response = await client.GetAsync(URL + "/api/Employees");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                ObservableCollection<Employee> employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(json);
                return employees;
            }
            return null;
        }
        public async Task UpdateEmployee(Employee e, string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            string input = JsonConvert.SerializeObject(e);
            HttpResponseMessage response = await client.PutAsync(URL+ "api/Employees", new StringContent(input, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await GetEmployees();
            }
        }
        public async Task AddEmployee(Employee e, string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            string input = JsonConvert.SerializeObject(e);
            HttpResponseMessage response = await client.PostAsync(URL + "api/Employees", new StringContent(input, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await GetEmployees();
            }
        }
        public async Task DeleteEmployee(Employee e, string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            HttpResponseMessage response = await client.DeleteAsync(URL + "api/Employees" + e.Id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await GetEmployees();
            }
        }

        #endregion

        #region Products

        public async Task<ObservableCollection<Products>> GetProducts(string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            HttpResponseMessage response = await client.GetAsync(URL + "api/products");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                ObservableCollection<Products> products = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);
                return products;
            }
            return null;
        }
 
        public async Task UpdateProduct(Products p, string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            string input = JsonConvert.SerializeObject(p);
            HttpResponseMessage response = await client.PutAsync("api/products", new StringContent(input, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await GetProducts();
            }
        }
        public async Task AddProduct(Products newProduct, string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            string input = JsonConvert.SerializeObject(newProduct);
            HttpResponseMessage response = await client.PostAsync("api/products", new StringContent(input, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await GetProducts();
            }
        }
        public async Task DeleteProduct(Products p, string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            HttpResponseMessage response = await client.DeleteAsync("api/products" + p.Id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await GetProducts();
            }
        }

        #endregion

    }
}


using Newtonsoft.Json;
using nmct.ba.cashlessproject.model.IT_Bedrijf;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Net;
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

        public async Task UpdatePassword(NewPassword np, string token)
        {
            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(token);
            string input = JsonConvert.SerializeObject(np);
            HttpResponseMessage response = await client.PutAsync("http://localhost:13160/api/WachtwoordWijzigen", new StringContent(input, Encoding.UTF8, "application/json"));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //await GetCustomers();
            }
        }

       

    }
}


using Newtonsoft.Json;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.Klant
{
    public class WebAcces
    {
        private static string URL = ConfigurationManager.AppSettings["apiUrl"];
        public static TokenResponse GetToken(string username, string password)
        {
            var client = new OAuth2Client(new Uri(URL + "token"));
            return client.RequestResourceOwnerPasswordAsync(username, password).Result;
        }
 
    }
}

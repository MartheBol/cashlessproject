using be.belgium.eid;
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
using System.Windows;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Medewerkers1.ViewModel
{
    class BestellingVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Bestelling"; }
        }

        public BestellingVM()
        {
            GetProducten();
        
    }

         private double _teBetalen;

         public double TeBetalen
         {
             get { return _teBetalen; }
             set { _teBetalen = value; OnPropertyChanged("TeBetalen"); }
         }

        private ObservableCollection<Products> _Product;

        public ObservableCollection<Products> Product
        {
            get { return _Product; }
            set { _Product = value; OnPropertyChanged("Product"); }

        }

        private Products _selectedProduct;
        public Products SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; OnPropertyChanged("SelectedProduct"); }
        }


        private ObservableCollection<Products> _gekozenProducten;

        public ObservableCollection<Products> GekozenProducten
        {
            get { return _gekozenProducten; }
            set { _gekozenProducten = value; OnPropertyChanged("GekozenProducten"); }
        }

        private Customers _customer;
        public Customers Customer
        {
            get { return _customer; }
            set { _customer = value; OnPropertyChanged("Customer"); }
        }

        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }


        public ICommand TerugCommand
        {
            get { return new RelayCommand(Terug); }
        }

        private void Terug()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new BeginschermVM());
        }
        public ICommand Scannen
        {
            get { return new RelayCommand(Reader); }
        }

        public async void Reader()
        {
            BEID_ReaderSet.initSDK();
            // access the eID card here
            if (BEID_ReaderSet.instance().readerCount() > 0)
            {
                BEID_ReaderContext readerContext = readerContext = BEID_ReaderSet.instance().getReader();
                if (readerContext != null)
                {
                    if (readerContext.getCardType() == BEID_CardType.BEID_CARDTYPE_EID)
                    {
                        Customers c = new Customers();
                        BEID_EIDCard card = readerContext.getEIDCard();
                        BEID_Picture picture;
                        picture = card.getPicture();
                        byte[] bytearray;
                        bytearray = picture.getData().GetBytes();
                        c.Picture = bytearray;
                        //
                        c.CardNumber = card.getID().getNationalNumber();
                        c.Address = card.getID().getStreet() + "," + card.getID().getZipCode() + " " + card.getID().getMunicipality();
                        c.CustomerName = card.getID().getFirstName() + " " + card.getID().getSurname();
                      
                        c.Sex = card.getID().getGender();
                        Customer = c;
                        //OnPropertyChanged("SelectedCustomer");
                        ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                        bool exists = await CheckIfCustomerExists(c.CardNumber);

                        if (exists == false)
                        {

                            Error = "klant is nog niet geregistreerd";
                        }
                        else
                        {
                            BestellingVM bestelling = new BestellingVM();
                            appvm.ChangePage(bestelling);
                        }
                    }
                }
            }
            BEID_ReaderSet.releaseSDK();
        }
        public async Task<bool> CheckIfCustomerExists(string KaartNummer)
        {
            var client = new HttpClient();
            //string natnr = Convert.ToString(nationalNumber);
            //client.SetBearerToken(token);
            HttpResponseMessage response = await client.GetAsync("http://localhost:13160/api/Klant?Kaartnummer=" + KaartNummer);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                //bool exists = JsonConvert.DeserializeObject<bool>(json);
                Customers c = JsonConvert.DeserializeObject<Customers>(json);
                SelectedCustomer = c;

                if (c.CustomerName != null)
                {
                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.ActiveUserId = c.Id;
                    bool exists = true;
                    return exists;
                }
            }
            return false;
        }

        private async void GetProducten()
        {
            using (HttpClient client = new HttpClient())
            {
                
                HttpResponseMessage response = await client.GetAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/productsKlanten/");
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Product = JsonConvert.DeserializeObject<ObservableCollection<Products>>(json);


                }
            }
        }

        ObservableCollection<Products> list = new ObservableCollection<Products>();     
        private void VoegToe()
        {
            if(SelectedProduct != null)
            {
                Products p = new Products();
                p=SelectedProduct;
                double prijs = p.Price;
                HoeveelTeBetalen(prijs);
                list.Add(p);
                GekozenProducten = list;

            }
        }
        private void HoeveelTeBetalen(double prijs)
        {
            TeBetalen += prijs;
        }

        public ICommand VoegToeCommand
        {
            get { return new RelayCommand(VoegToe); }
        }

        public ICommand AfrekenenCommand
        {
            get { return new RelayCommand(Afrekenen); }
        } 
        private void Afrekenen()
        {
            if(TeBetalen < SelectedCustomer.Balance)
            {
                foreach (Products p in GekozenProducten)
                {
                    Sales s = new Sales
                    {
                        Amount = 1,
                        CustomerID= SelectedCustomer.Id,
                        ProductID = p.Id,
                        RegisterID = 1,
                        Timestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds,
                        TotalPrice = p.Price
                    };
                    AddSale(s);
                }

                double nieuwBalance = SelectedCustomer.Balance - TeBetalen;
                Customers nieuweCustomer = new Customers
                {
                    Id = SelectedCustomer.Id,
                    Address = SelectedCustomer.Address,
                    CustomerName = SelectedCustomer.CustomerName,
                    Sex = SelectedCustomer.Sex,
                    Picture = SelectedCustomer.Picture,
                    Balance = nieuwBalance
                };
                UpdateKlant(nieuweCustomer);
                ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                appvm.ChangePage(new BestellingVM());

            }

            else
            {
                Error = "saldo ontoereikend";
            }
     
        }

           public async void AddSale(Sales s)
        {
            using (HttpClient client = new HttpClient())
            {
                string input = JsonConvert.SerializeObject(s);
                //client.SetBearerToken(ApplicationVM.token.AccessToken);
                HttpResponseMessage response = await client.PostAsync(ConfigurationManager.AppSettings["apiUrl"] + "api/sale/", new StringContent(input, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("De bestelling is afgerekend");

                }
                
            }
        }


           private async void UpdateKlant(Customers customer)
           {
               using (HttpClient client = new HttpClient())
               {
                   Customers kl = customer;
                   //client.SetBearerToken(ApplicationVM.token.AccessToken);
                   string input = JsonConvert.SerializeObject(kl);
                   HttpResponseMessage response = await client.PutAsync("http://localhost:13160/api/sale", new StringContent(input, Encoding.UTF8, "application/json"));
                   if (response.IsSuccessStatusCode)
                   {
                       ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                       appvm.ChangePage(new BestellingVM());
                   }
               }
           }


               
            }
        }
    

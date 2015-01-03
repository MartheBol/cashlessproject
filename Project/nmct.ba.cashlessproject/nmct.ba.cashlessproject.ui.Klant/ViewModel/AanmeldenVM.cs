using be.belgium.eid;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Klant.ViewModel
{
    class AanmeldenVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Aanmelden"; }
        }

        private Customers _customer;
        public Customers Customer
        {
            get { return _customer; }
            set { _customer = value; OnPropertyChanged("Customer"); }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }

        public ICommand AanmeldenCommand
        {
           get {return new RelayCommand(Reader);}
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
                        c.Address = card.getID().getStreet() + "," + card.getID().getZipCode() + " " + card.getID().getMunicipality() ;
                        c.CustomerName = card.getID().getFirstName() + " " + card.getID().getSurname();
                     
                        c.Sex = card.getID().getGender();
                        Customer = c;
                        //OnPropertyChanged("SelectedCustomer");
                        ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                        bool exists = await CheckIfCustomerExists(c.CardNumber);

                        if (exists == false)
                        {
                            RegisterGegevensVM klantRegisteren = new RegisterGegevensVM();
                            appvm.ChangePage(klantRegisteren);
                            Error = "U bent nog niet geregistreerd, gelieve dit eerst te doen.";
                        }
                        else
                        {
                             OpladenVM opladen = new OpladenVM();
                             appvm.ChangePage(opladen);
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
    }
}

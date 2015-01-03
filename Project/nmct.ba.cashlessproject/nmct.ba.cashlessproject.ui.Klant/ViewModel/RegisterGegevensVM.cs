using be.belgium.eid;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using nmct.ba.cashlessproject.model.Klanten;
using nmct.ba.cashlessproject.ui.Klant.View.Converters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Klant.ViewModel
{
    class RegisterGegevensVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "Gegevens"; }
        }
        private Customers _selectedCustomer;

        public Customers SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { _selectedCustomer = value; OnPropertyChanged("SelectedCustomer"); }
        }

        public ICommand LeesKaartCommand
        {
            get { return new RelayCommand(EidReader); }
        }

        public ICommand CustomerOpslaan
        {
            get { return new RelayCommand(Opslaan); }
        }
       
        public void EidReader()
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
                        c.Address = card.getID().getStreet() + ", " + card.getID().getZipCode() + " "+  card.getID().getMunicipality();
                        c.CustomerName = card.getID().getFirstName() + " " + card.getID().getSurname();
                        c.Sex = card.getID().getGender();
                        c.Country = card.getID().getCountry();
                        c.CardNumber = card.getID().getNationalNumber();
                        c.BirthDate = card.getID().getDateOfBirth();
                        SelectedCustomer = c;
                        
                        OnPropertyChanged("SelectedCustomer");
                    }
                }
            }
            BEID_ReaderSet.releaseSDK();
        }

        public async void Opslaan()
        {
            Customers nieuweKlant = SelectedCustomer;
            using (HttpClient client = new HttpClient())
            {
                //client.SetBearerToken(ApplicationVM.token.AccessToken);
                string kl = JsonConvert.SerializeObject(nieuweKlant);
                HttpResponseMessage response = await client.PostAsync("http://localhost:13160/api/Klant", new StringContent(kl, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
                    appvm.ChangePage(new OpladenVM());
                    appvm.ActiveUserId = SelectedCustomer.Id;
                }
            }
        }

        public ICommand RegistrerenAnnuleren
        {
            get { return new RelayCommand(Annuleren); }
        }

        private void Annuleren()
        {
            ApplicationVM appvm = App.Current.MainWindow.DataContext as ApplicationVM;
            appvm.ChangePage(new StartSchermVM());
        }





    }
}

using GalaSoft.MvvmLight.Command;
using nmct.ba.cashlessproject.model.Klanten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Thinktecture.IdentityModel.Client;

namespace nmct.ba.cashlessproject.ui.Medewerkers1.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public static TokenResponse token = null;

        public ApplicationVM()
        {

            Pages.Add(new BestellingVM());
           


            // Add other pages

            CurrentPage = new BeginschermVM();
        }

        private int _activeUserId;
        public int ActiveUserId
        {
            get { return _activeUserId; }
            set { _activeUserId = value; OnPropertyChanged("ActiveUserId"); }
        } 
        private object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private List<IPage> pages;
        public List<IPage> Pages
        {
            get
            {
                if (pages == null)
                    pages = new List<IPage>();
                return pages;
            }
        }

        public static Employee ActiveUser { get; set; }

      

        public ICommand ChangePageCommand
        {
            get { return new RelayCommand<IPage>(ChangePage); }
        }

        public void ChangePage(IPage page)
        {
            CurrentPage = page;
        }

    }
}
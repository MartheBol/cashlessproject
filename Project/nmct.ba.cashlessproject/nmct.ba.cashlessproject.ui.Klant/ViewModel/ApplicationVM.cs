using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.ui.Klant.ViewModel
{
    class ApplicationVM : ObservableObject
    {
        public ApplicationVM()
        {
            Pages.Add(new PageOneVM());
            //Pages.Add(new OpladenVM());


            ;
            // Add other pages

            CurrentPage = new StartSchermVM();
        }

        private object currentPage;
        public object CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged("CurrentPage"); }
        }

        private int _activeUserId;
        public int ActiveUserId
        {
            get { return _activeUserId; }
            set { _activeUserId = value; OnPropertyChanged("ActiveUserId"); }
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

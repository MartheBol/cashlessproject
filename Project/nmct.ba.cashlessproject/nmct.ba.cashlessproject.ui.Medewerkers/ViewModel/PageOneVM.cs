using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.ui.Medewerkers.ViewModel
{
    class PageOneVM : ObservableObject, IPage
    {
        public string Name
        {
            get { return "First Page"; }
        }

        private string _demo = "demo";

        public string Demo
        {
            get { return _demo; }
            set { _demo = value; OnPropertyChanged("Demo"); }
        }

    }

}

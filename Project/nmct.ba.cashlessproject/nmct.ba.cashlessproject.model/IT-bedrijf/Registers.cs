using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace nmct.ba.cashlessproject.model.IT_Bedrijf
{
    public class Registers
    {
        private int  _id;
        private string _registerName;
        private string _device;
        private DateTime _purcheseDate;
        private DateTime _expiresDate;
        public string  _organisationName;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayName("Naam kassa")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De naam moet tussen de 3 en 100 tekens bevatten")]
        [Required(ErrorMessage = "De naam van de kassa is verplicht in te vullen ")]
        public string RegisterName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }

        [DisplayName("Merk")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam moet tussen de 2 en 50 tekens bevatten")]
        [Required(ErrorMessage = "Het merk van de kassa is verplicht in te vullen ")]
        public string Device
        {
            get { return _device; }
            set { _device = value; }
        }

        [DisplayName("Aankoopdatum (dd/mm/yyyy" )]
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate
        {
            get { return _purcheseDate; }
            set { _purcheseDate = value; }
        }

        [DisplayName("Vervaldatum (dd/mm/yyyy)")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpiresDate
        {
            get { return _expiresDate; }
            set { _expiresDate = value; }
        }

        [DisplayName("Naam organisatie")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De naam moet tussen de 2 en 50 tekens bevatten")]
        
        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }




    }
}

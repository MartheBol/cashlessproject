using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.cashlessproject.model.IT_Bedrijf
{
    public class Organisations
    {
          private int _id;
        private string _login;
        private string _password;
        private string _dbName;
        private string _dbLogin;
        private string _dbPassword;
        private string _organisationName;
        private string _address;
        private string _email;
        private long _phone;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DisplayName("Gebruikersnaam")]
        [Required]
        [StringLength(50, MinimumLength=2, ErrorMessage="De gebruikersnaam moet tussen de 2 & 50 tekens bevatten")]
        [RegularExpression(@"^[a-zA-Z'-' ''-']{1,50}$", ErrorMessage = "Er zijn geen speciale tekens toegelaten")]
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }


        [DisplayName("Wachtwoord")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Het passwoord moet tussen de 2 & 50 tekens bevatten")]

        public string Password
        {
            get { return _password; }
            set { _password= value; }
        }

        [DisplayName("Databasenaam")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De  databsenaam moet tussen de 2 & 50 tekens bevatten")]

        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        [DisplayName("Database gebruikersnaam")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De loginnaam moet tussen de 2 & 50 tekens bevatten")]
        public string DbLogin
        {
            get { return _dbLogin; }
            set { _dbLogin = value; }
        }
        [DisplayName("Database wachtwoord")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "De wachtwoord moet tussen de 2 & 50 tekens bevatten")]
        public string DbPassword
        {
            get { return _dbPassword; }
            set { _dbPassword = value; }
        }

        [DisplayName("Naam organisatie")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "De verenigingnaam moet tussen de 2 & 50 tekens bevatten")]
        public string OrganisationName
        {
            get { return _organisationName; }
            set { _organisationName = value; }
        }

        [DisplayName("Adres")]
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Het adres moet tussen de 2 & 50 tekens bevatten")]
       
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        [DisplayName("Telefoonnummer")]
        [DataType(DataType.PhoneNumber)]
        public long Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
    }
}

    

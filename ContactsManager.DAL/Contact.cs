using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.DAL
{
    public class Contact
    {
        public string Nom { get; set; }//obligatoire?
        public string Prenom { get; set; }//obligatoire?
        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime DateDeNaissance { get; set; }


    }
}

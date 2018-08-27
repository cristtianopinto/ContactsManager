using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsManager.DAL;

namespace ContactsManager.Business
{
    public class ServiceContact : IServiceContact
    {
        public static void CreeContact()
        {

        }

		public IEnumerable<Contact> ChercherContacts(string texte)
		{
			throw new NotImplementedException();
		}

		public void CreerContact(Contact contact)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Contact> GetContacts()
		{
			throw new NotImplementedException();
		}

		public void SupprimerContact(Contact contact)
		{
			throw new NotImplementedException();
		}
	}
}

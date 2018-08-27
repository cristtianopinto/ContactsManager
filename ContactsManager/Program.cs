using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ContactsManager.DAL;
using ContactsManager.Business;

namespace ContactsManager
{        
    class Program
    {       
        
        static List<Contact> contacts = new List<Contact>();//Type générique - voir Nullable<T> TEMOS QUE MUDAR ISSO!!!   
		static IServiceContact service = new ServiceContact();//NOVA ABORDAGEM

		static void SaisierContact()
        {
            //Type génériques(page 160):
            //Nullable<DateTime> date = null;            
            try
            {
                //**//
                Contact c = new Contact();
                //**//
                PrintMenu("ADD CONTACT ","Add");                
                c.Nom = OutilsConsole.SaisirChaineObligatoire("\t- Tapez le nom du contact, svp(Obligatoire):");                
                c.Prenom = OutilsConsole.SaisirChaineObligatoire("\t- Tapez le Prénom, svp:(Obligatoire):");
                Console.WriteLine("\t- Tapez le Email, svp: ");
                c.Email = Console.ReadLine();
                Console.WriteLine("\t- Tapez le Téléphone, svp:    ");
                c.Telephone = Console.ReadLine();
                Console.WriteLine("\t- Tapez le Date de naissance, svp:(jj/mm/aaaa)");                
                c.DateDeNaissance = OutilsConsole.SaisierData();
                contacts.Add(c);
                Visual("Add");
                Console.WriteLine("\tContact Ajouté!");
                Visual("Normal");
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Visual("Danger");
                Console.WriteLine($"\tProblème d'entrée de données! {e.Message}");
                Visual("Normal");
                Console.ReadKey();
            }            
        }                          
        /// <summary>
        /// Methode qui permet de afficher des opction selectione ou pas selectione
        /// </summary>
        /// <param name="Opction"></param>
        /// <param name="op"></param>
        static void AfficherOpction(List<string> Opctions,int choix)
        {
            Console.WriteLine("\t╔═════════════════════════╗");
            for (int i =0; i < Opctions.Count ; i++)
            {

                if (i == choix)
                {
                    Visual("Add");
                   Console.WriteLine("\t║  »" + Opctions[i]+ "");                 
                    Visual("Normal");
                }
                else
                {
                    Visual("Normal");
                    Console.WriteLine("\t║  " + Opctions[i]+ "");
                }
            }
            Console.WriteLine("\t╚═════════════════════════╝");
        }       
        static void AjoutContact()
        {
            Console.CursorVisible = true;
            SaisierContact();           
        }
        static void PrintSingleContact(Contact c,int i)
        {
            
            Console.WriteLine($"{i + 1,-3}{c.Nom,-26}{c.Prenom,-26}" +
                                  $"{c.Email,-40}{c.Telephone,-26}" +
                                  $"{c.DateDeNaissance.ToShortDateString(),-26}");
        }
        static void PrintSingleContact(Contact c)
        {
            Console.WriteLine($"{"-",-3}{c.Nom,-26}{c.Prenom,-26}" +
                                  $"{c.Email,-40}{c.Telephone,-26}" +
                                  $"{c.DateDeNaissance.ToShortDateString(),-26}");
        }
        static void ListeContact()
        {
            Console.Clear();
            int consoleWidth = Console.WindowWidth;
            PrintMenu("LISTE DES CONTACTS", "Lister");
            Console.WriteLine("\t[Vous avez {0} contacts]", contacts.Count);
            Visual("EnTete");
            Console.WriteLine($"{"ID",-3 }{"NOM",-26}{"PRENOM",-26}{"EMAIL",-40}{"TELEPHONE",-26}{"DATE",-26}\n");
            Visual("Normal");
            for (int i = 0; i <= contacts.Count - 1; i++)
            {
                PrintSingleContact(contacts[i], i);
            }
        }
        static void SuppressionContact()
        {
            Console.CursorVisible = true;
            string contacSupression;
            int index_Contact_Supression;
            Console.Clear();            
            PrintMenu("SUPPRIMER CONTACT ", "Danger");
            Console.WriteLine($"{"NOM",-10} / {"PRENOM",-12}\n");
            foreach (Contact c in contacts)
            {
                Console.WriteLine($"{c.Nom,-10} / {c.Prenom,-12}");
            }
            Console.WriteLine("\n\t-Quel contact souhaitez-vous supprimer[Nom]?");
            contacSupression = Console.ReadLine();
            index_Contact_Supression = contacts.FindIndex(x => x.Nom==contacSupression);
            if (index_Contact_Supression < 0)
            {
                
                Console.WriteLine("Contact pas trouver!");
                
                Console.ReadKey();
            }
            else
            {
                contacts.RemoveAt(index_Contact_Supression);
                //FAIRE MESSAGE CONTACT SUPRESSION AVEC SUCESS
                PrintMenu("SUPRESSION CONTACT", "Danger");
                foreach (Contact c in contacts)
                {
                    Console.WriteLine($" Nom: {c.Nom,-10} / {c.Prenom}");
                }
                Visual("Add");
                Console.WriteLine("SUPPRIMÉ AVEC SUCCÈS!");
                Visual("Normal");
                Console.ReadKey();
            }            
        }
        static void TrierContacts()
        {
            Console.Clear();
            PrintMenu("TRIER CONTACTS", "Lister");
            bool sortir = false;
            List<string> menu = new List<string> { "TRIER PAR NOM", "TRIER PAR PRENOM"};
            while (true)
            { 
                switch (MenuChoix(menu, "TRIER CONTACTS", "Lister"))
                {
                    case 0:
                        TrierAvecRequeteContactNom();
                        sortir = true;
                        break;
                    case 1:
                        TrierAvecRequeteContactPrenom();
                        sortir = true;
                        break;                                      
                }
                if (sortir)
                {
                    break;
                }
            }              
            Console.ReadKey();
        }
        static void FiltrerContacts()
        {
            Console.Clear();
            PrintMenu("FILTRER CONTACTS", "Lister");
            //refaire
            Console.WriteLine("\n\t-Rechercher dans Nom et Prenom:");
            Console.CursorVisible = true;
            string recherche = Console.ReadLine();            
            RequeteContactNomPrenom(recherche);
            Console.ReadKey();
        }        
        public static void Visual(string Type)
        {
            switch (Type)
            {
                case "Normal":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "Danger":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Add":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "Lister":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "EnTete":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
            };           
        }
        static void RequeteContactNomPrenom(string entre_requete)
        {
            var requete = from contact in contacts
                          where contact.Nom.ToUpper().StartsWith(entre_requete.ToUpper()) ||
                                contact.Prenom.ToUpper().StartsWith(entre_requete.ToUpper())
                          select contact;
            Visual("EnTete");
            Console.WriteLine($"{"ID",-3 }{"NOM",-26}{"PRENOM",-26}{"EMAIL",-40}{"TELEPHONE",-26}{"DATE",-26}\n");
            Visual("Normal");
            foreach (var c in requete)
            {                
                PrintSingleContact(c);
            };
        }
        static void TrierAvecRequeteContactNom()
        {
            var requete = from contact in contacts
                          orderby contact.Nom ascending
                          select contact;
            Console.Clear();
            PrintMenu("TRIER CONTACTS", "Lister");
            Visual("EnTete");
            Console.WriteLine($"{"ID",-3 }{"NOM",-26}{"PRENOM",-26}{"EMAIL",-40}{"TELEPHONE",-26}{"DATE",-26}\n");
            Visual("Normal");
            foreach (var c in requete)
            {                
                PrintSingleContact(c);
            };
        }
        static void TrierAvecRequeteContactPrenom()
        {
            var requete = from contact in contacts
                          orderby contact.Prenom ascending
                          select contact;
            Console.Clear();
            PrintMenu("TRIER CONTACTS", "Lister");
            Visual("EnTete");
            Console.WriteLine($"{"ID",-3 }{"NOM",-26}{"PRENOM",-26}{"EMAIL",-40}{"TELEPHONE",-26}{"DATE",-26}\n");
            Visual("Normal");
            foreach (var c in requete)
            {                
                PrintSingleContact(c);
            };
        }
        /// <summary>
        /// Procedure principal de affichage de Menu.
        /// </summary>
        /// <returns>Retourne le choix de l'utilisateur.</returns>
        static void AfficherMenu(int op, List<string> list_menu, string tittle, string visual)
        {
            Console.Clear();
            PrintMenu(tittle, visual);
            Console.WriteLine("\t[Vous avez {0} contacts]\n\n", contacts.Count);
            AfficherOpction(list_menu, op);
        }
        public static void PrintMenu(string Tittle, string Type)
        {
            Console.Clear();
            char[] chars = { '█', '▓', '▒', '░' };
            Visual(Type);
            Console.Write(new string(chars[2], Console.WindowWidth));
            Console.Write(new string('-', Console.WindowWidth));
            Console.Write(new string(' ', Console.WindowWidth / 2 - Tittle.Length / 2) + Tittle + new string(' ', Console.WindowWidth / 2+1 - Tittle.Length / 2));
            Console.Write(new string('-', Console.WindowWidth));
            Console.Write(new string(chars[3], Console.WindowWidth));
            Visual("Normal");
        }
        static int MenuChoix(List<string> menu,string tittleMenu,string visualMenu)
        {
            int choix = 0;            
            ConsoleKey teclaPress = new ConsoleKey();            
            Console.CursorVisible = false;
            while (true)
            {
                AfficherMenu(choix, menu, tittleMenu, visualMenu);
                teclaPress = Console.ReadKey().Key;
                if (teclaPress == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {
                    if (teclaPress == ConsoleKey.DownArrow)
                    {
                        if (choix == menu.Count-1) { choix = 0; } else { choix++; }
                    }
                    if (teclaPress == ConsoleKey.UpArrow)
                    {
                        if (choix == 0) { choix = menu.Count-1; } else { choix--; }
                    }
                }
            }
            return choix;
        }
        static void Quitter()
        {
            DonneesContact.EcrireFichier(contacts);
            Environment.Exit(0);
        }
        static void LancerApplication()
        {
            Console.Title = "AGENDA";
           // Console.SetWindowPosition(,);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            DonneesContact.RecupererFichier(contacts);
            Visual("Normal");
        }
        static void Main(string[] args)
        {
            LancerApplication();
            bool sortir = false;
            List<string> menu = new List<string> { "ENREGISTREZ UN CONTACT", "LISTE CONTACTS", "SUPPRIMER CONTACT", "TRIER CONTACTS", "FILTRER CONTACTS", "SORTIE" };            
            while (true)
            {                
                //Chamar metodo de escolha
                switch (MenuChoix(menu,"MENU","Normal"))
                {
                    case 0:
                        AjoutContact();
                        //Console.ReadKey();
                        break;
                    case 1:
                        ListeContact();
                        Console.ReadKey();
                        break;
                    case 2:
                        SuppressionContact();
                        //Console.ReadKey();
                        break;
                    case 3:
                        TrierContacts();
                        break;
                    case 4:
                        FiltrerContacts();
                        break;
                    case 5:
                        sortir = true;                        
                        break;                    
                }
                if (sortir)
                {
                    break;
                }
            }
            Quitter();

        }
    }
}


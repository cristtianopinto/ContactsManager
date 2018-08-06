using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ContactsManager
{
    public class PeutEtreNull<T>
    {
        public T Valeur { get; set; }
        public bool PossedeValeur { get; set; }

    }
        
    class Program
    {
        const string DirFile = "contacts.txt";

        static List<Contact> contacts = new List<Contact>();//Type générique - voir Nullable<T>
           
        static void SaisierContact()
        {
            //Type génériques(page 160):
            Nullable<DateTime> date = null;
            
            try
            {
                Contact c = new Contact();
                
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
                Console.WriteLine("\tContact Ajouter!");
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
        /// Methode pour gardes les donnez sur un file txt
        /// </summary>
        static void EcrireFichier()
        {
            int i = 0;
            StreamWriter fileWriter = new StreamWriter(DirFile);
            foreach(Contact c in contacts)
            {
                
                fileWriter.WriteLine($"Contact {i}:");
                fileWriter.WriteLine("nom-" + c.Nom);
                fileWriter.WriteLine("prenom-" + c.Prenom);
                fileWriter.WriteLine("email-" + c.Email);
                fileWriter.WriteLine("tele-" + c.Telephone);
                fileWriter.WriteLine("date-"+c.DateDeNaissance);
                fileWriter.WriteLine("****************");
                i++;
            }
            fileWriter.Close();
            
        }
        /// <summary>
        /// Methode pour gardes les donnez sur un file txt
        /// </summary>
        static void RecupererFichier()
        {
            StreamReader fileReader = new StreamReader(DirFile);            
            string line;
            Contact c = new Contact();
            bool convertiondate = true;
            while((line = fileReader.ReadLine())!= null)
            {
                
                if (line.Contains("-"))
                {
                    string[] iniLine = line.Split('-');
                    switch (iniLine[0])
                    {
                        case "nom":                            
                            c.Nom = iniLine[1];
                            break;
                        case "prenom":
                            c.Prenom = iniLine[1];
                            break;
                        case "email":
                            c.Email= iniLine[1];
                            break;
                        case "tele":
                            c.Telephone = iniLine[1];
                            break;
                        case "date":
                            c.DateDeNaissance = OutilsConsole.ConvertirData(iniLine[1], out convertiondate);
                            break;
                        
                    }
                }else if (line.Contains("***"))
                {
                    contacts.Add(c);
                    c = new Contact();//VER O QUANTO É CORRETO FAZER ISSO
                }            
            }            
            fileReader.Close();
        }
        /// <summary>
        /// Methode qui permet de afficher des opction selectione ou pas selectione
        /// </summary>
        /// <param name="Opction"></param>
        /// <param name="op"></param>
        static void AfficherOpction(string[] Opctions,int choix)
        {
            for(int i =0; i < Opctions.Length ; i++)
            {
                if (i == choix)
                {
                    Visual("Add");
                    Console.WriteLine(">\t" + Opctions[i]);
                    Visual("Normal");
                }
                else
                {
                    Visual("Normal");
                    Console.WriteLine("\t" + Opctions[i]);
                }
            }            
        }
        /// <summary>
        /// Procedure principal de affichage de Menu.
        /// </summary>
        /// <returns>Retourne le choix de l'utilisateur.</returns>
        static void AfficherMenu(int op)
        {
            Console.Clear();
            string[] menu = new string[6] { "ENREGISTREZ UN CONTACT", "LISTE CONTACTS", "SUPPRIMER CONTACT", "TRIER CONTACTS", "FILTRER CONTACTS", "SORTIE" };
            
            PrintMenu("MENU","Normal");
            Console.WriteLine("\t[Vous avez {0} contacts]\n\n",contacts.Count);
            AfficherOpction(menu, op);
            
        }
        public static void PrintMenu(string Tittle, string Type)
        {
            Console.Clear();
            char[] chars = { '█', '▓', '▒', '░' };
            Visual(Type);           
            Console.Write(new string(chars[2], Console.WindowWidth));           
            Console.Write(new string('-', Console.WindowWidth));
            Console.Write(new string(' ', Console.WindowWidth/2 - Tittle.Length/2) + Tittle + new string(' ', Console.WindowWidth/2 - Tittle.Length/2));
            Console.Write(new string('-', Console.WindowWidth));
            Console.Write(new string(chars[3], Console.WindowWidth));
            Visual("Normal");
        }
        static void AjoutContact()
        {
            Console.CursorVisible = true;
            SaisierContact();           
        }
        static void ListeContact()
        {
            Console.Clear();
            PrintMenu("LISTE DES CONTACTS", "Lister");
            Console.WriteLine("\t[Vous avez {0} contacts]\n", contacts.Count);
            
            Console.WriteLine($"{"ID",-3}{"NOM",-10}{"PRENOM",-12}{"EMAIL",-30}{"TELEPHONE",-12}{"DATE",-10}\n");
            for (int i = 0; i <= contacts.Count - 1; i++)
            {               
                Console.WriteLine($"{i+1,-3}{contacts[i].Nom,-10}{contacts[i].Prenom,-12}" +
                                  $"{contacts[i].Email,-30}{contacts[i].Telephone,-12}" +
                                  $"{contacts[i].DateDeNaissance.ToShortDateString(),-10}\n");
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
            Console.WriteLine("\n\t-Quel contact souhaitez-vous supprimer?");
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
            
            Console.WriteLine("\t\t\n\nConstruction!");
            
            Console.ReadKey();

        }
        static void FiltrerContacts()
        {
            Console.Clear();
            PrintMenu("FILTRER CONTACTS", "Lister");
            
            Console.WriteLine("\t\t\n\nConstruction!");
            
            Console.ReadKey();
        }
        static void Quitter()
        {
            //voir demain
            //FAIRE ICI ECRITURE DU FICHIER
            Environment.Exit(0);
        }
        static void LancerApplication()
        {
            //FAIRE ICI LA LECTURE DU FICHIER
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
            };           
        }
        
        static void Main(string[] args)
        {
            //RemplirListContacts();
            Console.Title = "AGENDA";
            Console.SetWindowPosition(0, 0);
            //Console.SetWindowSize(40,70);
            RecupererFichier();
            int choix = 0;
            Visual("Normal");
            //string choixUtilisateur;
            bool sortir = false;
            ConsoleKey teclaPress = new ConsoleKey();
            while (true)
            {
                Console.CursorVisible = false;
                while (true)
                {
                    AfficherMenu(choix);
                    teclaPress = Console.ReadKey().Key;
                    if (teclaPress == ConsoleKey.Enter)
                    {
                        break;
                    }
                    else
                    {
                        if(teclaPress == ConsoleKey.DownArrow)
                        {
                            if(choix == 5) { choix = 0; } else { choix++; }
                        }
                        if (teclaPress == ConsoleKey.UpArrow)
                        {
                            if (choix == 0) { choix = 5; } else { choix--; }
                        }
                    }
                    
                }               
                //Chamar metodo de escolha
                switch (choix)
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
                        EcrireFichier();
                        break;
                    default:
                        Console.WriteLine("OP INVALIDE");//DESUSO
                        Console.ReadKey();
                        break;


                }
                if (sortir)
                {
                    break;
                }
            }
        }
    }
}


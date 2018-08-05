using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ContactsManager
{
    class Program
    {
        const string DirFile = "contacts.txt";

        static List<Contact> contacts = new List<Contact>();

        static void RemplirListContacts()
        {
            Contact a1 = new Contact();
            Contact a2 = new Contact();
            Contact a3 = new Contact();
            a1.Nom = "PINTO";a1.Prenom = "cris";a1.Email = "cristtianopinto@gmail.com";a1.Telephone = "0769804157";a1.DateDeNaissance = new DateTime(1985,07,06);
            a2.Nom = "DOMINGUES"; a2.Prenom = "caroline"; a2.Email = "domingues@gmail.com"; a2.Telephone = "0969804157"; a2.DateDeNaissance = new DateTime(1980, 06, 01);
            a3.Nom = "SOUZA"; a3.Prenom = "alice"; a3.Email = "tavares@gmail.com"; a3.Telephone = "0169804157"; a3.DateDeNaissance = new DateTime(1990, 09, 12);
            contacts.Add(a1);
            contacts.Add(a2);
            contacts.Add(a3);
        }
        //static List<int> listeInt = new List<int>();
        //static List<DateTime> listeDate = new List<DateTime>();
        //static List<bool> listeBool = new List<bool>();


        //static StreamWriter ecrire = new StreamWriter("contacts.txt");
        //static StreamReader file = new StreamReader(@"contacts.txt");
        static void SaisierContact()
        {
            try
            {
                Contact c = new Contact();

                PrintMenu("ADD CONTACT","Add");
              
                c.Nom = SaisirChaineObligatoire("\t- Tapez le nom du contact, svp(Obligatoire):");
                c.Prenom = SaisirChaineObligatoire("\t- Tapez le Prénom, svp:(Obligatoire):");

                Console.WriteLine("\t- Tapez le Email, svp: ");
                c.Email = Console.ReadLine();
                Console.WriteLine("\t- Tapez le Téléphone, svp:    ");
                c.Telephone = Console.ReadLine();
                Console.WriteLine("\t- Tapez le Date de naissance, svp:(jj/mm/aaaa)");                
                c.DateDeNaissance = SaisierData();
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
        static string SaisirChaineObligatoire(string message)
        {
            Console.WriteLine(message);
            var saisie = Console.ReadLine();
            while (string.IsNullOrEmpty(saisie))
            {
                PrintMenu("ADD CONTACT","Add");
                Console.WriteLine(message);
                Visual("Danger");
                Console.WriteLine(new string(' ', Console.WindowWidth / 2) + "Champ requis. Recommence:");
                Visual("Normal");
                saisie = Console.ReadLine();
            }
            return saisie;
        }

        /// <summary>
        /// Méthode pour convertir la date sans s'arrêter
        /// </summary>
        /// <param name="chaine"></param>
        /// <param name="booldata"></param>
        /// <returns></returns>
        static DateTime ConvertirData(string chaine, out bool booldata)
        {
            DateTime date_valuer = new DateTime();
            booldata = DateTime.TryParse(chaine, out date_valuer);            
            return date_valuer;
        }
        static DateTime SaisierData()
        {
            bool saisiValide;
            DateTime date = ConvertirData(Console.ReadLine(), out saisiValide);
            while (!saisiValide)
            {
                Console.WriteLine("Veuillez entrer une date valide (jj / mm / aaaa)!");
                date = ConvertirData(Console.ReadLine(), out saisiValide);
            }
            return date;

        }
        static void SaisierEntier()
        {
            //listeInt.Add(int.Parse(Console.ReadLine()));
        }
        static void SaisierBoolean()
        {
            //listeBool.Add(bool.Parse(Console.ReadLine()));
        }
        static void SaisierDate()
        {
            //listeDate.Add(DateTime.Parse(Console.ReadLine()));
        }
        static void SaisierString()
        {
            //contacts.Add(Console.ReadLine());
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
                            c.DateDeNaissance = ConvertirData(iniLine[1], out convertiondate);
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
            string[] menu = new string[4] { "ENREGISTREZ UN CONTACT", "LISTE CONTACTS", "SUPPRIMER CONTACT", "SORTIE" };
            //char[] chars = { '█', '▓', '▒', '░' };
           //Console.WriteLine("\n"+new string(chars[3], Console.WindowWidth));
            PrintMenu("MENU","Normal");
            Console.WriteLine("\tVous avez {0} contacts.\n\n",contacts.Count);
            AfficherOpction(menu, op);
            
        }
        static void PrintMenu(string Tittle, string Type)
        {
            Console.Clear();
            Visual(Type);
            Console.WriteLine("\n" + new string('-', Console.WindowWidth));
            Console.WriteLine(new string(' ', Console.WindowWidth/2 - Tittle.Length/2) + Tittle + new string(' ', Console.WindowWidth/2 - Tittle.Length/2));
            Console.WriteLine(new string('-', Console.WindowWidth)+"\n");
            Visual("Normal");
        }
        static void AjoutContact()
        {
            Console.CursorVisible = true;
            SaisierContact();
            //SaisierString();
            //SaisierEntier();
            //ecrire.WriteLine(valeur_contact);
            //ecrire.Close();
        }
        static void ListeContact()
        {
            Console.Clear();
            PrintMenu("LISTE DE CONTACTS", "Lister");
            Console.WriteLine("\tVous avez {0} contacts\n", contacts.Count);
            
            Console.WriteLine($"{"ID",-3}{"NOM",-10}{"PRENOM",-12}{"EMAIL",-30}{"TELEPHONE",-12}{"DATE",-10}\n");
            for (int i = 0; i <= contacts.Count - 1; i++)
            {
                //Console.WriteLine("{0} - nom: {1,15} - nombre: {2,10}", i+1, contacts[i], listeInt[i]);
                //Console.WriteLine($"{i + 1} - nom: {contacts[i],-15} - nombre: {listeInt[i],-10}");
               
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
            /*
            foreach (string c in contacts)
            {
                Console.WriteLine(c);
            }
            */
           
            PrintMenu("SUPPRIMER CONTACT", "Danger");
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
            /*
            if (contacts.Contains(contacSupression))
            {
                listeInt.RemoveAt(contacts.IndexOf(contacSupression));
                contacts.Remove(contacSupression);
            }
            else
            {
                Console.WriteLine("      Contac Inexistent      ");
                Console.ReadKey();
            }
            */

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
        static void Visual(string Type)
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
                            if(choix == 3) { choix = 0; } else { choix++; }
                        }
                        if (teclaPress == ConsoleKey.UpArrow)
                        {
                            if (choix == 0) { choix = 3; } else { choix--; }
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


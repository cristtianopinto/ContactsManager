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
        static List<string> contacts = new List<string>();
        static List<int> listeInt = new List<int>();
        static List<DateTime> listeDate = new List<DateTime>();
        static List<bool> listeBool = new List<bool>();


        //static StreamWriter ecrire = new StreamWriter("contacts.txt");
        //static StreamReader file = new StreamReader(@"contacts.txt");

        static void SaisierEntier()
        {
            listeInt.Add(int.Parse(Console.ReadLine()));
        }
        static void SaisierBoolean()
        {
            listeBool.Add(bool.Parse(Console.ReadLine()));
        }
        static void SaisierDate()
        {
            listeDate.Add(DateTime.Parse(Console.ReadLine()));
        }
        static void SaisierString()
        {
            contacts.Add(Console.ReadLine());
        }
        /// <summary>
        /// Methode pour gardes les donnez sur un file txt
        /// </summary>
        static void EcrireFichier(string addressFichier)
        {

        }
        /// <summary>
        /// Methode pour gardes les donnez sur un file txt
        /// </summary>
        static void RecupererFichier(string addressFichier)
        {

        }
        /// <summary>
        /// Procedure principal de affichage de Menu.
        /// </summary>
        /// <returns>Retourne le choix de l'utilisateur.</returns>
        static void AfficherMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\n--------------------------------------");
            Console.WriteLine("---              MENU              ---");
            Console.WriteLine("        Vous avez {0} contacts     ", contacts.Count);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("-- OP1 AJOUTER CONTACT  : TAPEZ [1] --");
            Console.WriteLine("-- OP2 LISTER CONTACTS  : TAPEZ [2] --");
            Console.WriteLine("-- OP3 SUPRIMER CONTACT : TAPEZ [3] --");
            Console.WriteLine("-- OP4 QUITTER          : TAPEZ [9] --");
        }
        static void AjoutContact()
        {
            Console.Clear();
            VisualAdd();
            Console.WriteLine("\n\n-------------------------------------");
            Console.WriteLine("---          ADD CONTACT          ---");
            Console.WriteLine("-------------------------------------");
            VisualNormal();
            Console.WriteLine("    Tapez le nom du contact, svp:    ");
            SaisierString();
            Console.WriteLine("    Tapez le nombre, svp:    ");
            SaisierEntier();
            //ecrire.WriteLine(valeur_contact);
            //ecrire.Close();
        }
        static void ListeContact()
        {
            Console.Clear();
            VisualLister();
            Console.WriteLine("\n\n-------------------------------------");
            Console.WriteLine("---         LISTE CONTACT         ---");
            Console.WriteLine("-------------------------------------");
            VisualNormal();
            Console.WriteLine("        Vous avez {0} contacts     ", contacts.Count);
            Console.WriteLine("       Voila tout les contacts:      ");

            for (int i = 0; i <= contacts.Count - 1; i++)
            {
                //Console.WriteLine("{0} - nom: {1,15} - nombre: {2,10}", i+1, contacts[i], listeInt[i]);
                Console.WriteLine($"{i + 1} - nom: {contacts[i],15} - nombre: {listeInt[i],10}");
            }
        }
        static void SuppressionContact()
        {
            string contacSupression;
            Console.Clear();
            foreach (string c in contacts)
            {
                Console.WriteLine(c);
            }
            VisualDanger();
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("---      SUPRESSION CONTACT       ---");
            Console.WriteLine("-------------------------------------");
            VisualNormal();
            Console.WriteLine("    Tapez le nom du contact, svp:    ");
            contacSupression = Console.ReadLine();
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
        static void VisualNormal()
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void VisualDanger()
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void VisualAdd()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void VisualLister()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void Main(string[] args)
        {
            VisualNormal();
            string choixUtilisateur;
            bool sortir = false;
            while (true)
            {
                AfficherMenu();
                choixUtilisateur = Console.ReadLine();
                switch (choixUtilisateur)
                {
                    case "1":
                        AjoutContact();
                        //Console.ReadKey();
                        break;
                    case "2":
                        ListeContact();
                        Console.ReadKey();
                        break;
                    case "3":
                        SuppressionContact();
                        //Console.ReadKey();
                        break;
                    case "9":
                        sortir = true;
                        break;
                    default:
                        Console.WriteLine("OP INVALIDE");
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


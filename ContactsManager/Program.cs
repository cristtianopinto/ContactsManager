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
        static StreamWriter ecrire = new StreamWriter("contacts.txt");

        /// <summary>
        /// Procedure principal de affichage de Menu.
        /// </summary>
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
            string valeur_contact;
            Console.Clear();
            Console.WriteLine("\n\n-------------------------------------");
            Console.WriteLine("---          ADD CONTACT          ---");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("    Tapez le nom du contact, svp:    ");
            valeur_contact = Console.ReadLine();
            contacts.Add(valeur_contact);
            ecrire.WriteLine(valeur_contact);
            ecrire.Close();
        }
        static void ListeContact()
        {
            Console.Clear();
            Console.WriteLine("\n\n-------------------------------------");
            Console.WriteLine("---         LISTE CONTACT         ---");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("        Vous avez {0} contacts     ", contacts.Count);
            Console.WriteLine("       Voila tout les contacts:      ");
            
            foreach (string c in contacts)
            {
                Console.WriteLine(c);
            }
            
        }
        static void SuppressionContact()
        {
            Console.Clear();
            foreach (string c in contacts)
            {
                Console.WriteLine(c);
            }
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("---      SUPRESSION CONTACT       ---");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("    Tapez le nom du contact, svp:    ");

            contacts.Remove(Console.ReadLine());
        }
        static void Quitter()
        {
            
        }
        static void Visual()
        {
           
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void Main(string[] args)
        {
            Visual();
            string op;
            bool sortir=false;
            while (true)
            {
                AfficherMenu();
                op = Console.ReadLine();
                switch(op)
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

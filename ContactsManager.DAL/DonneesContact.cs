using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactsManager;
namespace ContactsManager.DAL
{
    public static class DonneesContact
    {
        const string DirFile = "contacts.txt";
        /// <summary>
        /// Methode pour gardes les donnez sur un file txt
        /// </summary>
        public static void EcrireFichier(List<Contact> contacts)
        {
            /*Aprouche professeur
             * System.IO
             * System.Text
             * Class StringBuilder
             * append()
             * File.WriteAllText(chemin du fichier,contenu)
             * */
            int i = 0;
            StreamWriter fileWriter = new StreamWriter(DirFile);
            foreach (Contact c in contacts)
            {

                fileWriter.WriteLine($"Contact {i}:");
                fileWriter.WriteLine("nom-" + c.Nom);
                fileWriter.WriteLine("prenom-" + c.Prenom);
                fileWriter.WriteLine("email-" + c.Email);
                fileWriter.WriteLine("tele-" + c.Telephone);
                fileWriter.WriteLine("date-" + c.DateDeNaissance);
                fileWriter.WriteLine("****************");
                i++;
            }
            fileWriter.Close();

        }
        /// <summary>
        /// Methode pour gardes les donnez sur un fichier *.txt
        /// </summary>
        public static void RecupererFichier(List<Contact> contacts)
        {
            /*Aprouche professeur
             * File.ReadAllText(chemin du fichier)
             * */
            StreamReader fileReader = new StreamReader(DirFile);
            string line;
            Contact c = new Contact();
            while ((line = fileReader.ReadLine()) != null)
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
                            c.Email = iniLine[1];
                            break;
                        case "tele":
                            c.Telephone = iniLine[1];
                            break;
                        case "date":
                            //c.DateDeNaissance = DateTime.Parse(iniLine[1]);
                            c.DateDeNaissance = DateTime.Parse(iniLine[1]);
                            break;
                    }
                }
                else if (line.Contains("***"))
                {
                    contacts.Add(c);
                    c = new Contact();//VER O QUANTO É CORRETO FAZER ISSO
                }
            }
            fileReader.Close();
        }
    }
}

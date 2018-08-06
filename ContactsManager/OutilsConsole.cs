using System;
using System.Threading;

namespace ContactsManager
{
    public static class OutilsConsole
    {
        private static bool continu_Thread = true;

        public static int SaisirEntier(string Message,string MessageOblige)
        {
            Console.WriteLine(Message);
            int i;
            bool ok = int.TryParse(Console.ReadLine(),out i) ;
            while (!ok)
            {
                Console.WriteLine(MessageOblige);
            }
            return i;
        }
        public static DateTime ConvertirData(string chaine, out bool booldata)
        {
            DateTime date_valuer = new DateTime();
            booldata = DateTime.TryParse(chaine, out date_valuer);
            return date_valuer;
        }
        public static DateTime SaisierData()
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

        public static string SaisirChaineObligatoire(string Message)
        {
            Console.WriteLine(Message);
            string saisie = Console.ReadLine();
            /*
            //TESTE DE THREAD
            var th = new Thread(AttendEsc);
            th.IsBackground = true;
            th.Start();
            Thread.Sleep(200);
            */
            while (string.IsNullOrEmpty(saisie))
            {  
                Program.PrintMenu("ADD CONTACT ", "Add");
                Console.WriteLine(Message);
                Program.Visual("Danger");
                Console.WriteLine(new string(' ', Console.WindowWidth / 2) + "Champ requis. Recommence:");
                Program.Visual("Normal");
                saisie = Console.ReadLine();
            }
            return saisie;
        }

        private static void AttendEsc()
        {
            if (Console.ReadKey().Key == ConsoleKey.Escape)
            {
                Console.WriteLine("TESTE");
                
                Console.ReadKey();
            }

        }

    }
}


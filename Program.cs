using Malshinon.DAL;
using Malshinon.Helpers;
using Malshinon.UI;
using System;

namespace Malshinon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string connectionString = "server=localhost;user=root;database=malshinon;port=3306;";

            // Initialize DB
            try
            {
                Init.Initialise(connectionString);
                Logger.Log("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to initialize application: " + ex.Message);
                return;
            }

            // Main loop
            while (true)
            {
                Menu.ShowMenu();
                string input = Console.ReadLine();

                if (input == "0")
                {
                    Console.WriteLine("Exiting system. Goodbye.");
                    break;
                }

                Runner.Run(input);

                Console.WriteLine("\nPress Enter to return to menu...");
                Console.ReadLine();
            }
        }
    }
}

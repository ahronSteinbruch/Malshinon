using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.UI
{
    internal static class Menu
    {
        public static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Malshinon — Community Intel Reporting System");
            Console.WriteLine("1. Submit a new report");
            Console.WriteLine("2. Show potential informants");
            Console.WriteLine("3. Show dangerous targets");
            Console.WriteLine("0. Exit");
            Console.Write("\nSelect an option: ");
        }
    }
}

using Malshinon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Malshinon.UI
{
    internal static class NewReport
    {
        public static void addReport()
        {
            Console.WriteLine("enter your code name");
            string CodeOrName = Console.ReadLine();

            int reporterId = PersonRegistry.GetOrCreateReporter(CodeOrName);

            Console.WriteLine("Enter message about target");
            string newMessage = Console.ReadLine();

            List<int> targetsID = new();
            Console.WriteLine("Enter the namse of targets");
            string names = Console.ReadLine();

            foreach(string name in names.Split(" ")){
                targetsID.Add(PersonRegistry.GetOrCreateTarget(name));
            }



            

            




            

        }
    }
}
/*Id INT PRIMARY KEY AUTO_INCREMENT,
                 ReporterId INT,
                 Message TEXT,
                 Timestamp DATETIME,
                 Tag VARCHAR(100),
                 FOREIGN KEY (ReporterId) REFERENCES Reporters(Id)*/
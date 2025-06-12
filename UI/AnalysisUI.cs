using Malshinon.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon.UI
{
    internal static class AnalysisUI
    {
        public static void ShowPotentialInformants()
        {
            foreach(var item in ReporterRepository.GetAllPotentialInformants())
            {
                Console.WriteLine(item.Name);
            }
        }
        public static void ShowDangerousTargets()
        {
            foreach (var item in TargetRepository.GetAll())
            {
                if (Target.IsDangerous(item.Id) || Target.HasBurstActivity(item.Id))
                {
                    Console.WriteLine(item.Name);
                }
                
            }
        }
    }
}

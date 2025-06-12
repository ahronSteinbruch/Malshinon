using Malshinon.DAL;
using Malshinon.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Malshinon.Services
{
    public static class PersonRegistry
    {

        public static int ReporteridGenerator = ReporterRepository.getHighestId();
        public static int TargetidGenerator = TargetRepository.getHighestId();
        // -- מודיע
        public static int GetOrCreateReporter(string nameOrCode)
        {
            var res = ReporterRepository.GetByNameOrBySecretCode(nameOrCode);
            if (res != null) 
                return res.Id;
            Reporter newReporter = new Reporter
            {
                Id = ++ReporteridGenerator,
                Name = nameOrCode,
            };
            ReporterRepository.AddReporter(newReporter);
            return newReporter.Id;
            
        }

        // -- מטרה
        public static int GetOrCreateTarget(string nameOrCode)
        {

            int res = TargetRepository.GetIdByNameOrBySecretCode(nameOrCode);
            if (res != -1)
                return res;
            Target newTarget = new Target
            {
                Id = ++TargetidGenerator,
                Name = nameOrCode,
            };
            return TargetRepository.InsertAndReturnId(newTarget);
        }
    }
}

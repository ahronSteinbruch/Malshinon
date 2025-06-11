using Malshinon.DAL;
using Malshinon.models;
using Malshinon.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Malshinon.Services
{
    public static class PersonRegistry
    {

        public static int idGenerator = ReporterRepository.getHighestId();
        // -- מודיע
        public static int GetOrCreateReporter(string nameOrCode)
        {
            var res = ReporterRepository.GetByNameOrBySecretCode(nameOrCode);
            if (res != null) 
                return res.Id;
            Reporter newReporter = new Reporter
            {
                Id = ++idGenerator,
                Name = nameOrCode,
            };
            ReporterRepository.AddReporter(newReporter);
            return newReporter.Id;
            
        }

        // -- מטרה
        public static Target GetOrCreateTarget(string nameOrCode)
        {
          
            var res = TargetRepository.

            return target;
        }
    }
}

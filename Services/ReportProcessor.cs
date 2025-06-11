using Malshinon.DAL;
using Malshinon.models;


namespace Malshinon.Services
{
    public static class ReportProcessor
    {
        
        private static readonly TagAnalyzer _tagAnalyzer;

        private static readonly int reporter_id;

        private static readonly AlertGenerator _alertGenerator;



        public static 


            // 2. איתור/יצירת מטרות
            var targets = targetCodesOrNames
                .Select(PersonRegistry.GetOrCreateTarget)
                .ToList();

            // 3. ניתוח תגית ראשית
            var primaryTag = _tagAnalyzer.AnalyzeTextAndFindPrimaryTag(text);

            // 4. יצירת הדוח
  

            // 5. קישור מטרות לדוח
           

            // 6. עידכון סטטוס גיוס


            // 7. בדיקת התרעה (נבדוק רק עבור כל מטרה בנפרד — נחזיר רק אחת)
           
            // 8. אין התרעה

        }
    }
}


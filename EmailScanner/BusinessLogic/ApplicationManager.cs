using EmailScanner.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailScanner.BusinessLogic
{
    public class ApplicationManager
    {
        private static readonly object padlock = new object();
        private static ApplicationManager instance = null;

        HashSet<string> _collHash = null;

        public static ApplicationManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ApplicationManager();
                        instance.PopulateWordsCollections();
                    }
                    return instance;
                }
            }
        }

        private void PopulateWordsCollections()
        {
            var coll = Utility.ReadFile("DrugSlang.txt");

            string[] stringSeparators = new string[] { "\r\n" };
            var list = coll.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();
            _collHash = new HashSet<string>(list);
        }

        internal void AnalyzeEmailBody(string body)
        {
            string[] stringSeparators = new string[] { "\r\n", " " };
            var list = body.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToList();

            bool isExist = _collHash.Overlaps(list);
        }
    }
}

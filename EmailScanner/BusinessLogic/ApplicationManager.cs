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

        public static ApplicationManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ApplicationManager();
                    }
                    return instance;
                }
            }
        }
    }
}

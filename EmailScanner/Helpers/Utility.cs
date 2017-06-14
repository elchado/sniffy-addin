using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EmailScanner.Helpers
{
    public class Utility
    {
        private static string GetInstallFolder()
        {
            try
            {
                //Get the assembly information
                Assembly assemblyInfo = Assembly.GetExecutingAssembly();

                //Location is where the assembly is run from 
                string assemblyLocation = assemblyInfo.Location;

                //CodeBase is the location of the Install deployment files
                Uri uriCodeBase = new Uri(assemblyInfo.CodeBase);
                string InstallFolderLocation = Path.GetDirectoryName(uriCodeBase.LocalPath.ToString());
                return InstallFolderLocation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static string GetFile(string fileName)
        {
            var file = Path.Combine(GetInstallFolder() + "\\Words", fileName);
            if (File.Exists(file))
            {
                return file;
            }
            return null;
        }

        public static string ReadFile(string fileName)
        {
            var filePath = GetFile(fileName);
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd();
                }
            }
            return null;
        }
    }
}

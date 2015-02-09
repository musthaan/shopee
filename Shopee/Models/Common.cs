using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;

namespace Shopee.Models
{
    public static class Common
    {
        public static bool CreateFolderIfNotExists(string Path)
        {
            try
            {
                if (!Directory.Exists(Path))
                { DirectoryInfo di = Directory.CreateDirectory(Path); }
                return true;
            }
            catch (Exception ioex)
            {
                Console.WriteLine(ioex.Message);
            }
            return false;
        }

        internal static void LogError(Exception ex)
        {

        }

        internal static string GetWebConfig(string name)
        {
            if (ConfigurationManager.AppSettings[name] != null)
            {
                return ConfigurationManager.AppSettings[name];
            }
            return string.Empty;
        }
         
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Prorent24.Common.ApplicationSettings
{
    public static class DirectorySettings
    {
        public static string currentDirectory = Directory.GetCurrentDirectory();
        public static string wwwrootDirectory = string.Concat(currentDirectory, "\\wwwroot");
        public static string backupDirectory = string.Concat(wwwrootDirectory, "\\Backups\\");
        public static string logsDirectory = string.Concat(wwwrootDirectory, "\\LOGS\\");
        public static string fileDirectory = string.Concat(wwwrootDirectory, "\\Files\\");
        public static string scriptDirectory = string.Concat(wwwrootDirectory, "\\Scripts\\");
        public static string reportDirectory = string.Concat(scriptDirectory, "\\Reports\\");
        public static string dataBasesDirectory = string.Concat(currentDirectory, "\\DataBases\\");
        public static string appDataDirectory = string.Concat(wwwrootDirectory, "\\AppData\\");
        public static string dataBaseEmptyPath = string.Concat(appDataDirectory, "empty.db");
        public static string connStringToEmptyDb ="Filename=wwwroot\\AppData\\empty.db";

        public static string frontEnvDirectory = String.Empty;
        public static string frontEnvProdDirectory = "Prorent24.App\\dist\\";
        public static string frontEnvDevDirectory = "Prorent24.App\\src\\";
        public static string frontPathToI18n= "assets\\i18n\\";
    }
}

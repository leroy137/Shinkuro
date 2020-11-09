using System;
using System.IO;
using System.Reflection;

namespace Shinkuro.Services
{
    public static class FileManager
    {

        public static String LogoFolderPathPrefix { get; set; } = @"Resources\Images\Logotips";
        public static String AppDir { get; set; }
        public static String LogoFolderPath { get; set; }
        public static int MaxSizeLogo { get; set; }

        static FileManager()
        {
            AppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LogoFolderPath = Path.Combine(AppDir, LogoFolderPathPrefix);

            if (!Directory.Exists(LogoFolderPath))
                Directory.CreateDirectory(LogoFolderPath); 
        }
    }
}

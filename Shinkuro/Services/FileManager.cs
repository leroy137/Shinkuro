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
        public static int MaxSizeLogo { get; set; } = 2_097_152;

        static FileManager()
        {
            AppDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            LogoFolderPath = Path.Combine(AppDir, LogoFolderPathPrefix);

            if (!Directory.Exists(LogoFolderPath))
                Directory.CreateDirectory(LogoFolderPath); 
        }

        public static String UploadLogoCompetition(String filepath)
        {
            String pathNew = "";

            if (!File.Exists(filepath))
                throw new Exception($"Ошибка, файл {filepath} не существует!");

            String filenameNew = DateTime.Now.GetHashCode().ToString() + Path.GetFileName(filepath);
            pathNew = Path.Combine(LogoFolderPath,filenameNew);

            File.Copy(filepath, pathNew);
            return pathNew;
        }
    }
}

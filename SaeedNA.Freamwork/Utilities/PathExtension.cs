using System.IO;

namespace SaeedNA.Application.Utilities
{
    public static class PathExtension
    {
        public static string ServerPath = Directory.GetCurrentDirectory();

        public static string AppSettingsPath = Path.Combine(ServerPath, "appsettings.json");
        public static string AppSettingsDevPath = Path.Combine(ServerPath, "appsettings.Development.json");

        public static string UploadPath = "/uploads/";
        public static string UploadPathServer = Path.Combine(ServerPath, "wwwroot/uploads/");
    }
}
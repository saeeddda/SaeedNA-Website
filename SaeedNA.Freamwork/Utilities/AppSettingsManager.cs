using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.IO;

namespace SaeedNA.Application.Utilities
{
    public static class AppSettingsManager
    {
        private static JsonSerializerSettings GetJsonSettings()
        {
            JsonSerializerSettings settings = new();
            settings.Converters.Add(new ExpandoObjectConverter());
            settings.Converters.Add(new StringEnumConverter());
            return settings;
        }

        public static dynamic GetAppSettings(bool IsDevelopment = false)
        {
            var jsonText = File.ReadAllText(IsDevelopment ? PathExtension.AppSettingsDevPath : PathExtension.AppSettingsPath);
            return JsonConvert.DeserializeObject<ExpandoObject>(jsonText, GetJsonSettings());
        }

        public static void SetAppSettings(object value, bool IsDevelopment = false)
        {
            var newJson = JsonConvert.SerializeObject(value, Formatting.Indented, GetJsonSettings());
            File.WriteAllText(IsDevelopment ? PathExtension.AppSettingsDevPath : PathExtension.AppSettingsPath, newJson);
        }
    }
}

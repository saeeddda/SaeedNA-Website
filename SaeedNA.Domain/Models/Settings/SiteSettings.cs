using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Models.Settings
{
    public class SiteSettings
    {
        [Key]
        public int SettingId { get; set; }
        public string SettingName { get; set; }
        public string  SettingValue { get; set; }
    }
}
using SaeedNA.Domain.Models.MService;
using SaeedNA.Domain.Models.Resume;
using System.Collections.Generic;

namespace SaeedNA.Service.ViewModels
{
    public class AboutMeViewModel
    {
        public ICollection<History> History { get; set; }
        public ICollection<Skill> Skill { get; set; }
        public ICollection<ServiceCounter> ServiceCounter { get; set; }
        public ICollection<MyService> MyService { get; set; }
        public SiteSettingViewModel SiteSettings { get; set; }
    }
}

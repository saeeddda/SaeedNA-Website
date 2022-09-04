using SaeedNA.Data.DTOs.MService;
using SaeedNA.Data.DTOs.Resume;
using SaeedNA.Data.DTOs.Site;
using System.Collections.Generic;

namespace SaeedNA.Application.ViewModels
{
    public class AboutMeViewModel
    {
        public HistoryFilterDTO History { get; set; }
        public SkillFilterDTO Skill { get; set; }
        public CounterFilterDTO  Counter { get; set; }
        public MyServiceFilterDTO MyService { get; set; }
        public ICollection<PersonalInfoGetSetDTO> PersonalInfos { get; set; }
    }
}

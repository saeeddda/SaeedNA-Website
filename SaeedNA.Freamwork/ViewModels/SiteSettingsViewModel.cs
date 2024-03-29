﻿using SaeedNA.Data.DTOs.Site;
using System.Collections.Generic;

namespace SaeedNA.Application.ViewModels
{
    public class SiteSettingsViewModel
    {
        public ICollection<SettingGetSetDTO>  Settings { get; set; }
        public ICollection<PersonalInfoGetSetDTO> PersonalInfos { get; set; }
        public ICollection<SeoGetSetDTO> Seos { get; set; }
    }
}

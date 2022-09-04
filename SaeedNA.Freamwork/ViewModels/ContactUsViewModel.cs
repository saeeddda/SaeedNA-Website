using SaeedNA.Data.DTOs.Site;
using System.Collections.Generic;

namespace SaeedNA.Application.ViewModels
{
    public class ContactUsViewModel
    {
        public ICollection<PersonalInfoGetSetDTO> PersonalInfos { get; set; }
        public SocialMediaFilterDTO SocialMedia { get; set; }
    }
}

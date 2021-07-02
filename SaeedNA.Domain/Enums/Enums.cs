using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Enums
{
    public enum SkillProgressType
    {
        [Display(Name ="گرد")]
        Circular ,
        [Display(Name = "خطی")]
        Strip
    }
}

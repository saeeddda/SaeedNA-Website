using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Models.OnlineManager
{
    public class OnlineUser
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "کد یونیک")]
        public string UserGuid { get; set; }
        [Display(Name = "آی پی")]
        public string UserIp { get; set; }
        [Display(Name = "تاریخ")]
        public string VisitDate { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Domain.Models.Email
{
    public class Email
    {
        [Key]
        public int EmailId { get; set; }
        [Display(Name = "نام")]
        public string EmailName { get; set; }
        [Display(Name = "شماره تماس")]
        public string EmailPhone { get; set; }
        [Display(Name = "ایمیل")]
        public string EmailEmail { get; set; }
        [Display(Name = "متن")]
        public string EmailText { get; set; }
    }
}

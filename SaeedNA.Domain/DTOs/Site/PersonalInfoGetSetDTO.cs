using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Site
{
    public class PersonalInfoGetSetDTO
    {
        public long PersonalInfoId { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FullName { get; set; }

        [Display(Name = "تاریخ تولد")]
        [MaxLength(15, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Birthday { get; set; }

        [Display(Name = "موبایل")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "درباره من")]
        public string AboutMe { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "شماره تلفن")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "سن")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Age { get; set; }

        [Display(Name = "زبان")]
        [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Language { get; set; }

        [Display(Name = "ملیت")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Nationality { get; set; }

        [Display(Name = "تصویر رزومه")]
        public string ResumeImage { get; set; }

        [Display(Name = "تصویر آواتار")]
        public string AvatarImage { get; set; }

        [Display(Name = "فایل رزومه")]
        public string ResumeFile { get; set; }

        [Display(Name = "پیشفرض")]
        public bool IsDefault { get; set; }
    }
}
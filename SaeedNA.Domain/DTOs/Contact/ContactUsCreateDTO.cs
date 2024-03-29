﻿using System.ComponentModel.DataAnnotations;

namespace SaeedNA.Data.DTOs.Contact
{
    public class ContactUsCreateDTO
    {
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FullName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Email { get; set; }

        [Display(Name = "موبایل")]
        [MaxLength(15, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        [MaxLength(64, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Subject { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفاً {0} را وارد کنید")]
        public string Text { get; set; }

        [Display(Name = "خوانده شده؟")]
        public bool IsRead { get; set; }
    }
}
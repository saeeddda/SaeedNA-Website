using System.Collections.Generic;
using SaeedNA.Data.DTOs.Paging;
using SaeedNA.Data.Entities.Contact;

namespace SaeedNA.Data.DTOs.Contact
{
    public class ContactUsFilterDTO : BasePaging
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }

        public List<ContactUs> ContactUs { get; set; }

        public ContactUsFilterDTO SetContactUs(List<ContactUs> contactUs)
        {
            this.ContactUs = contactUs;
            return this;
        }

        public ContactUsFilterDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;
            this.HowManyBeforeAndAfter = paging.HowManyBeforeAndAfter;
            return this;
        }
    }
}
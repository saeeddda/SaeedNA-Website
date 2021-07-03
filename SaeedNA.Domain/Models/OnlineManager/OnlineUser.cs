using System;
using System.Collections.Generic;
using System.Text;

namespace SaeedNA.Domain.Models.OnlineManager
{
    public class OnlineUser
    {
        public int UserId { get; set; }
        public string UserGuid { get; set; }
        public string UserIp { get; set; }
        public string VisitDate { get; set; }
    }
}

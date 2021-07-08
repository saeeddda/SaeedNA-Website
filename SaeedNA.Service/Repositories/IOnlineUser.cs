using SaeedNA.Domain.Models.OnlineManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaeedNA.Service.Repositories
{
    public interface IOnlineUser
    {
        void AddOnlineUser(OnlineUser onlineUser);
        ICollection<OnlineUser> GetAllOnlineUsers();
        ICollection<OnlineUser> GetOnlineUsersByIP(string ipAddress);
        ICollection<OnlineUser> GetOnlineUsersByDate(string startDateTime, string endDateTime);
        int GetOnlineUserCount();
        int GetOnlineUserCount(string startDateTime,string endDateTime);
    }
}

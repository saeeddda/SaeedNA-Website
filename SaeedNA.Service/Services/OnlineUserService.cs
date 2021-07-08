using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.OnlineManager;
using SaeedNA.Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaeedNA.Service.Services
{
    public class OnlineUserService : IOnlineUser
    {
        private readonly SaeedNAContext _context;

        public OnlineUserService(SaeedNAContext context)
        {
            _context = context;
        }

        public void AddOnlineUser(OnlineUser onlineUser)
        {
            _context.OnlineUsers.Add(onlineUser);
            _context.SaveChanges();
        }

        public ICollection<OnlineUser> GetAllOnlineUsers()
        {
            return _context.OnlineUsers.ToList();
        }

        public int GetOnlineUserCount()
        {
            return _context.OnlineUsers.Count();
        }

        public int GetOnlineUserCount(string startDateTime, string endDateTime)
        {
            return _context.OnlineUsers
                .Where(c => c.VisitDate.CompareTo(startDateTime) >= 0 && c.VisitDate.CompareTo(endDateTime) <= 0)
                .Count();
        }

        public ICollection<OnlineUser> GetOnlineUsersByDate(string startDateTime, string endDateTime)
        {
            return _context.OnlineUsers
                .Where(c => c.VisitDate.CompareTo(startDateTime) >= 0 && c.VisitDate.CompareTo(endDateTime) <= 0)
                .ToList();
        }

        public ICollection<OnlineUser> GetOnlineUsersByIP(string ipAddress)
        {
            return _context.OnlineUsers
                .Where(u => u.UserIp == ipAddress)
                .ToList();
        }
    }
}

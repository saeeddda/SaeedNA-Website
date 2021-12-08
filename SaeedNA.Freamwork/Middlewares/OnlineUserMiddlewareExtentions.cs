using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using SaeedNA.Service.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SaeedNA.Application.Middlewares
{
    public static class OnlineUserMiddlewareExtentions
    {
        public static void UseOnlineUsers(
            this IApplicationBuilder app,
            string cookieName = "UserGuid",
            int lastActivity = 20)
        {
            app.UseMiddleware<OnlineUserMiddleware>(cookieName, lastActivity);
        }
    }

    public class OnlineUserMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _cookieName;
        private readonly int _latActivity;
        private static readonly ConcurrentDictionary<string, bool> _allKey = new ConcurrentDictionary<string, bool>();

        public OnlineUserMiddleware(RequestDelegate next, string cookieName = "UserGuid", int lastActivity = 20)
        {
            _next = next;
            _cookieName = cookieName;
            _latActivity = lastActivity;
        }

        public Task InvokeAsync(HttpContext context, IMemoryCache memoryCache)
        {
            if(context.Request.Cookies.TryGetValue(_cookieName, out var userGuid) == false)
            {
                userGuid = Guid.NewGuid().ToString();
                context.Response.Cookies.Append(_cookieName, userGuid, new CookieOptions { HttpOnly = true, MaxAge = TimeSpan.FromDays(30) });
            }

            memoryCache.GetOrCreate(userGuid, cacheEntry =>
            {
                if(_allKey.TryAdd(userGuid, true) == false)
                {

                    cacheEntry.AbsoluteExpiration = DateTimeOffset.MinValue;
                }
                else
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(_latActivity);
                    cacheEntry.RegisterPostEvictionCallback(RemoveKeyWenExpire);
                    //onlineUser.AddOnlineUser(new OnlineUser() { UserGuid = userGuid, UserIp = context.Connection.RemoteIpAddress.ToString(), VisitDate = ConvertToShamsi(DateTime.Now) });

                }

                return string.Empty;
            });

            return _next(context);
        }

        private void RemoveKeyWenExpire(object key, object value, EvictionReason reson, object state)
        {
            string strKey = (string)key;
            if(!_allKey.TryRemove(strKey, out _))
                _allKey.TryUpdate(strKey, false, true);
        }

        public static int GetOnlineUsersCount()
        {
            return _allKey.Count(p => p.Value);
        }

        private string ConvertToShamsi(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(dateTime) + "/" + pc.GetMonth(dateTime).ToString("00") + "/" +
                   pc.GetDayOfMonth(dateTime).ToString("00");
        }
    }
}

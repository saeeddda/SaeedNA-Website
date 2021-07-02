using SaeedNA.Domain.Models.MService;
using System.Collections.Generic;

namespace SaeedNA.Service.Repositories
{
    public interface IMyService
    {
        void AddMyService(MyService myService);
        void UpdateMyService(MyService myService);
        ICollection<MyService> GetAllMyService();
        MyService GetMyServiceById(int id);
        void DeleteMyService(int id);
    }
}

using SaeedNA.Service.Repositories;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.MService;
using System.Collections.Generic;
using System.Linq;

namespace SaeedNA.Service.Services
{
    public class MyServiceService : IMyService
    {
        private readonly SaeedNAContext _context;

        public MyServiceService(SaeedNAContext context)
        {
            _context = context;
        }

        public void AddMyService(MyService service)
        {
            _context.MyServices.Add(service);
            _context.SaveChanges();
        }

        public void DeleteMyService(int id)
        {
            var ser = _context.MyServices.Find(id);
            _context.MyServices.Remove(ser);
            _context.SaveChanges();
        }

        public ICollection<MyService> GetAllMyService()
        {
            return _context.MyServices.ToList();
        }

        public MyService GetMyServiceById(int id)
        {
            return _context.MyServices.FirstOrDefault(s => s.MyServiceId == id);
        }

        public void UpdateMyService(MyService service)
        {
            _context.MyServices.Update(service);
            _context.SaveChanges();
        }
    }
}

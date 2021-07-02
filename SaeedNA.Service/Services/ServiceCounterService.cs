using SaeedNA.Service.Repositories;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.MService;
using System.Collections.Generic;
using System.Linq;

namespace SaeedNA.Service.Services
{
    public class ServiceCounterService : IServiceCounter
    {
        public readonly SaeedNAContext _context;

        public ServiceCounterService(SaeedNAContext context)
        {
            _context = context;
        }

        public void AddServiceCounter(ServiceCounter serviceCounter)
        {
            _context.ServiceCounters.Add(serviceCounter);
            _context.SaveChanges();
        }

        public void DeleteServiceCounter(int id)
        {
            var serviceCounter = _context.ServiceCounters.Find(id);
            _context.ServiceCounters.Remove(serviceCounter);
            _context.SaveChanges();
        }

        public ICollection<ServiceCounter> GetAllServiceCounter()
        {
            return _context.ServiceCounters.ToList();
        }

        public ServiceCounter GetServiceCounterById(int id)
        {
            return _context.ServiceCounters.FirstOrDefault(f => f.ServiceCounterId == id);
        }

        public void UpdateServiceCounter(ServiceCounter serviceCounter)
        {
            _context.ServiceCounters.Update(serviceCounter);
            _context.SaveChanges();
        }
    }
}

using SaeedNA.Domain.Models.MService;
using System.Collections.Generic;

namespace SaeedNA.Service.Repositories
{
    public interface IServiceCounter
    {
        void AddServiceCounter(ServiceCounter serviceCounter);
        void UpdateServiceCounter(ServiceCounter serviceCounter);
        ICollection<ServiceCounter> GetAllServiceCounter();
        ServiceCounter GetServiceCounterById(int id);
        void DeleteServiceCounter(int id);
    }
}

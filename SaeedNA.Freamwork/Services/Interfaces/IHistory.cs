using SaeedNA.Domain.Models.Resume;
using System.Collections.Generic;

namespace SaeedNA.Service.Repositories
{
    public interface IHistory
    {
        void AddHistory(History history);
        void UpdateHistory(History history);
        ICollection<History> GetAllHistory();
        History GetHistoryById(int id);
        void DeleteHistory(int id);
    }
}

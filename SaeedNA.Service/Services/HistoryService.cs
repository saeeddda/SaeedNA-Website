using SaeedNA.Service.Repositories;
using SaeedNA.Data.Context;
using SaeedNA.Domain.Models.Resume;
using System.Collections.Generic;
using System.Linq;

namespace SaeedNA.Service.Services
{
    public class HistoryService : IHistory
    {
        public readonly SaeedNAContext _context;

        public HistoryService(SaeedNAContext context)
        {
            _context = context;
        }

        public void AddHistory(History history)
        {
            _context.Histories.Add(history);
            _context.SaveChanges();
        }

        public void DeleteHistory(int id)
        {
            var history = _context.Histories.Find(id);
            _context.Histories.Remove(history);
            _context.SaveChanges();
        }

        public ICollection<History> GetAllHistory()
        {
            return _context.Histories.ToList();
        }

        public History GetHistoryById(int id)
        {
            return _context.Histories.FirstOrDefault(e => e.HistoryId == id);
        }

        public void UpdateHistory(History history)
        {
            _context.Histories.Update(history);
            _context.SaveChanges();
        }
    }
}

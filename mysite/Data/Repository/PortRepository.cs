using mysite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Data.Repository
{
    public class PortRepository : IPortRepository
    {
        private readonly DataContext _context;

        public PortRepository(DataContext context)
        {
            _context = context;
        }

        public void AddPortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Add(portfolio);
        }

        public List<Portfolio> GetAllPortfolios()
        {
            return _context.Portfolios.ToList();
        }

        public Portfolio GetPort(int id)
        {
            return _context.Portfolios.FirstOrDefault(p =>p.Id == id);
        }

        public void RemovePost(int id)
        {
            _context.Portfolios.Remove(GetPort(id));
        }

        public void UpdatePortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}

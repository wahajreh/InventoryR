using mysite.Models;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Data.Repository
{
    public class TruckRepository : ItruckRepository
    {
        private readonly DataContext _context;

        public TruckRepository(DataContext context)
        {
            _context = context;
        }
        public void AddTruck(Truck truck)
        {
            _context.Trucks.Add(truck);
        }

        public List<Truck> GetAllTrucks()
        {
            return _context.Trucks.ToList();
        }

        public IndexViewModel GetAllTrucks(int pageNumber, string category, string search)
        {
            Func<Truck, bool> InCategory = (truck) => { return truck.Category.ToLower().Equals(category.ToLower()); };

            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _context.Trucks.AsQueryable();

            if (!String.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));

            int trucksCount = query.Count();

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = trucksCount > skipAmount + pageSize,
                Category = category,
                Trucks = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()
            };
        }

        public Truck GetTruck(int id)
        {
            return _context.Trucks.FirstOrDefault(t => t.Id == id);
        }

        public void RemoveTruck(int id)
        {
            _context.Trucks.Remove(GetTruck(id));
        }

        public void UpdateTruck(Truck truck)
        {
            _context.Trucks.Update(truck);
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

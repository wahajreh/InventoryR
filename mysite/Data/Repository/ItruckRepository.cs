using mysite.Models;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Data.Repository
{
    public interface ItruckRepository
    {
        Truck GetTruck(int id);
        List<Truck> GetAllTrucks();
        IndexViewModel GetAllTrucks(int pageNumber, string category, string search);
        void AddTruck(Truck truck);
        void UpdateTruck(Truck truck);
        void RemoveTruck(int id);
        Task<bool> SaveChangesAsync();
    }
}

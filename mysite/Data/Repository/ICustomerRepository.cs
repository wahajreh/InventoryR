using mysite.Models;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Data.Repository
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(int id);
        List<Customer> GetAllCustomers();
        IndexViewModel GetAllCustomers(int pageNumber, string search);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void RemoveCustomer(int id);
        Task<bool> SaveChangesAsync();

    }
}

using mysite.Models;
using mysite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysite.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context)
        {
            _context = context;
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public IndexViewModel GetAllCustomers(int pageNumber, string search)
        {
   
            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _context.Customers.AsQueryable();

            int customersCount = query.Count();

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = customersCount > skipAmount + pageSize,
                Customers = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()
            };
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public void RemoveCustomer(int id)
        {
            _context.Customers.Remove(GetCustomer(id));
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
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

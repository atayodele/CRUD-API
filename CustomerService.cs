using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudApi
{
    public class CustomerService : ICustomerService
    {
        private readonly DataContext _context;

        public CustomerService(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Id == id);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.OrderByDescending(c => c.Id).ToListAsync();
        }

        public async Task<bool> CustomerExists(string email) 
        {
            var customer = await _context.Customers.AnyAsync(x => x.Email == email);
            if (customer)
                return true;
            return false;
        }
    }
}

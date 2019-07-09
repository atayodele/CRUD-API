using CrudApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApi
{
    public interface ICustomerService
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<bool> CustomerExists(string email); 
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id); 
    }
}

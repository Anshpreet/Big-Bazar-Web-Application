using BigBazarEntities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBazarRespository
{
     public interface IUserRepository
    {

        public Task<User> LoginUser(User user);

        public bool AddCustomerEmail(string email);
        public Task<int> GetLastCustomerId(string emailId);
        public Task<bool> AddCustomer(Customer customer);
        public Task<bool> AddPurchasedProducts(List<Product> products, List<Customer> customers);
        public Task<bool> UpdateProduct(List<Product> p);
        public Task<Reciept> GetLastReciept();
        public Task<List<Customer>> GetAllCustomers();
    }
}

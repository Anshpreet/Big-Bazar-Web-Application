using BigBazarEntities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBazarServices
{
    public interface IUserServices
    {
        public Task<User> LoginUser(User user);

        public Task<bool> AddCustomer(Customer customer);
        public Task<bool> AddPurchasedProducts(List<Product> products);
        public Task<Reciept> GetLastReciept();

        public Task<List<Customer>> GetAllCustomers();
    }
}

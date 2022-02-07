using BigBazarEntities.Entity;
using BigBazarRespository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBazarServices
{
    public class UserServices : IUserServices
    {

        IUserRepository _UserRepository;

        public UserServices(IUserRepository userRepository)
        {
            _UserRepository = userRepository;
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            List<Customer> customers = await _UserRepository.GetAllCustomers();

            int id =  _UserRepository.GetLastCustomerId(customer.EmailId).Result;

            if (id == 0)
            {
                return await _UserRepository.AddCustomer(customer);
            }
            else
            {
                return  _UserRepository.AddCustomerEmail(customer.EmailId);
            }
        }

        public async Task<bool> AddPurchasedProducts(List<Product> products)
        {
            List<Customer> customers = await _UserRepository.GetAllCustomers();

             await _UserRepository.AddPurchasedProducts(products, customers);

            return await _UserRepository.UpdateProduct(products);
            
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            return await _UserRepository.GetAllCustomers();
        }


        public async Task<Reciept> GetLastReciept()
        {
            return await _UserRepository.GetLastReciept();
        }

        

        public async Task<User> LoginUser(User user)
        {
            return await _UserRepository.LoginUser(user);

        }

        
    }
}

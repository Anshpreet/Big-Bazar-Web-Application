using BigBazarEntities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BigBazarRespository;

namespace BigBazarServices
{
    public class AdminServices : IAdminServices
    {
        IAdminRepository _AdminRepository;

        public AdminServices(IAdminRepository adminRepository)
        {
            _AdminRepository = adminRepository;
        }


        public async Task<bool> AddCategory(Category category)
        {
           return await _AdminRepository.AddCategory(category);
        }

        public  async Task<bool> AddProduct(Product product)
        {
            return await _AdminRepository.AddProduct(product);
        }

        public async Task<bool> AddUser(User user)
        {
            return await _AdminRepository.AddUser(user);
        }

        public async Task<bool> DeleteUser(User user)
        {
            return await _AdminRepository.DeleteUser(user);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _AdminRepository.GetAllProducts();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _AdminRepository.GetAllUsers();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _AdminRepository.GetCategories();
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            return await _AdminRepository.GetProductsByCategory(categoryId);
        }

        public async Task<User> GetUserById(int id)
        {
            return await _AdminRepository.GetUserById(id);
        }

        public async Task<User> LoginAdmin(User user)
        {
            return await _AdminRepository.LoginAdmin(user);
        }
    }
}

using BigBazarEntities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBazarServices
{
    public interface IAdminServices
    {

        public Task<bool> AddUser(User user);

        public Task<User> LoginAdmin(User user);

        public Task<bool> DeleteUser(User user);

        public Task<User> GetUserById(int id);

        public Task<List<User>> GetAllUsers();
        public Task<bool> AddCategory(Category category);

        public Task<List<Category>> GetCategories();

        public Task<bool> AddProduct(Product product);

        public Task<List<Product>> GetAllProducts();

        public Task<List<Product>> GetProductsByCategory(int categoryId);
    }
}

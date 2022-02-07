using BigBazarEntities.Entity;
using BigBazarRepository;
using CustomException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BigBazarRespository
{
    public class AdminRepository : IAdminRepository
    {

        private BigBazarContext _Context;
        public AdminRepository() { }
        public AdminRepository(BigBazarContext bigBazarContext)
        {
            _Context = bigBazarContext; 
        }
        public async Task<bool> AddCategory(Category category)
        {
            try
            {
                int rows = 0;
                _Context.Add(category);
                rows = await _Context.SaveChangesAsync();
                if (rows == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                int rows = 0;
                _Context.Add(product);
                rows = await _Context.SaveChangesAsync();
                if (rows == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task<bool> AddUser(User user)
        {
            try
            {
                int rows = 0;
                user.IsDeleted = false;
                _Context.Add(user);
                rows = await _Context.SaveChangesAsync();
                if (rows == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task<bool> DeleteUser(User user)
        {
            try
            {
                User user1 = await _Context.Users.FirstOrDefaultAsync(s => s.UserId == user.UserId);
                if (user1 != null)
                {
                    int rows = 0;
                    user1.IsDeleted = true;
                    rows = await _Context.SaveChangesAsync();
                    if (rows == 0)
                        return false;
                    else
                        return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured", ex);
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                List<Product> products = await _Context.Products.ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                List<User> users  = await _Context.Users.ToListAsync();
                
                return users;
            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task<List<string>> GetCategoriesNames()
        {
            try
            {
                List<Category> categories = await _Context.Categories.ToListAsync();
                List<string> categoriesList = new List<string>();
                foreach (Category c in categories)
                {
                    categoriesList.Add(c.CategoryName);
                }
                return categoriesList;
            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }


        public async Task<List<Category>> GetCategories()
        {
            try
            {
                List<Category> categories = await _Context.Categories.ToListAsync();
                
                return categories;
            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            try
            {
                List<Product> products = await _Context.Products.ToListAsync();
                List<Product> productsByCategory = new List<Product>();
                foreach(Product p in products)
                {
                    if (p.CategoryId == categoryId)
                    {
                        productsByCategory.Add(p);
                    }
                }
                return productsByCategory;
            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task<bool> UpdateProduct(Product p)
        {

            try
            {
                //List<Product> products =await _Context.Products.ToListAsync();
                //Product product = await _Context.Products.SingleOrDefaultAsync(s => s.ProductId==p.ProductId);
                Product product = await _Context.Products.FirstOrDefaultAsync(s => s.ProductId == p.ProductId);

                int rows = 0;
                product.ChangedQuantity = (product.ChangedQuantity - p.Quantity);
                rows = await _Context.SaveChangesAsync();
                if (rows == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task<User> GetUserById(int id)
        {
            try
            {
                User user = new User();
                List<User> users = await _Context.Users.ToListAsync();
                foreach (User u in users)
                {
                    if (u.UserId == id)
                    {
                        user.UserId = u.UserId;
                        user.UserName = u.UserName;
                        user.Password = u.Password;
                        user.Role = u.Role;
                    }
                }
                return user;
            }
            catch(Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
            
        }

        public async Task<User> LoginAdmin(User user)
        {
            bool flag = false;
            List<User> users = await _Context.Users.ToListAsync();
            foreach(User u in users)
            {
                if(u.UserId==user.UserId && u.Password.Equals(user.Password) && u.Role == true)
                {
                    flag = true;
                    user = u;
                    break;
                }
            }
            if (flag == true)
                return user;
            else
                throw new InvalidUserException("Incorrect User Id or Password.");
        }
    }
}

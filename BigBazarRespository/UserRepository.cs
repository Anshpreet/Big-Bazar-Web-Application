using BigBazarEntities.Entity;
using BigBazarRepository;
using CustomException;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigBazarRespository
{
    public class UserRepository : IUserRepository
    {

        static string emailId = null;
        public BigBazarContext _Context;
        public UserRepository() { }
        public UserRepository(BigBazarContext bigBazarContext) 
        {
            _Context = bigBazarContext;
        }


        public bool AddCustomerEmail(string email)
        {
            emailId = email;
            return true;
        }

        public async Task<bool> AddCustomer(Customer customer)
        {
            try
            {
                UserRepository obj = new UserRepository();
                emailId = customer.EmailId;
                int rows = 0;
                _Context.Add(customer);
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

        public async Task<bool> AddPurchasedProducts(List<Product> products, List<Customer> customers)
        {
                UserRepository obj = new UserRepository();
                 DateTime date= DateTime.Today;
                 int customerId =  GetLastCustomerId(emailId).Result;
                 int rows = 0;
                 Reciept reciept = new Reciept();
                 reciept.Date = date;
                 reciept.CustomerId = customerId;
                 int TotalQuantity = 0;
                 int TotalBill = 0;
                foreach(Product p in products)
                {
                    TotalBill =TotalBill+ p.Price;
                    TotalQuantity = TotalQuantity + p.Quantity;

                    Purchase purchase = new Purchase();
                    purchase.ProductId = p.ProductId;
                    purchase.Quantity = p.Quantity;
                    purchase.Price = p.Price;
                    purchase.DateOfPurchase = date;
                    purchase.CustomerId = customerId;
                    
                    _Context.Add(purchase);
                    rows = await _Context.SaveChangesAsync();
                   

            }

                 reciept.TotalBill = TotalBill;
                 reciept.Quantity = TotalQuantity;
                   _Context.Add(reciept);
                 rows = await _Context.SaveChangesAsync();

                return true;

            
        }

        public async Task<bool> UpdateProduct(List<Product> p)
        {

            try
            {
               
                foreach (Product product1 in p)
                {
                    Product product = await _Context.Products.FirstOrDefaultAsync(s => s.ProductId == product1.ProductId);

                    int rows = 0;
                    product.ChangedQuantity = (product.ChangedQuantity - product1.Quantity);
                    rows = await _Context.SaveChangesAsync();
                }
                
                    return true;
            }
            catch (Exception ex)
            {
                throw new SqlException("server error occured!", ex);
            }
        }

        public async Task< int> GetLastCustomerId( string emailId )
        {
            
            Customer customer = await  _Context.Customers.FirstOrDefaultAsync(c => c.EmailId.Equals(emailId));
            if (customer == null)
                return 0;
            else 
                return customer.CustomerId;

        }


        public async Task<Reciept> GetLastReciept()
        {
            int count = 0;
            Reciept reciept = new Reciept();
            List<Reciept> reciepts= await _Context.Reciepts.ToListAsync();
            foreach (Reciept c in reciepts)
            {
                if (count == (reciepts.Count - 1))
                {
                    reciept = c;
                }
                else
                    count++;
            }
            return reciept;
        }


        public async Task<List<Customer>> GetAllCustomers() 
        {
            
                List<Customer> customers = await _Context.Customers.ToListAsync();

                return customers;
           
        }

       

        public async Task<User> LoginUser(User user)
        {
            try
            {
                bool flag = false;
                List<User> users = await _Context.Users.ToListAsync();
                foreach (User u in users)
                {
                    if (u.UserId == user.UserId && u.Password.Equals(user.Password) && u.Role == false)
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
            }catch(Exception ex)
            {
                throw new SqlException("server error", ex);
            }
        }

    }
}

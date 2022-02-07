using BigBazarWebApplication.Helper;
using BigBazarWebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BigBazarWebApplication.Controllers
{
    public class UserMvcController : Controller
    {
         static List<ProductModel> cartProducts = new List<ProductModel>();

        BigBazarApi _api = new BigBazarApi();


        public ActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(UserModel user)
        {

            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<UserModel>("User/LoginUser", user);
            postTask.Wait();
            if(postTask.Result.ReasonPhrase =="Bad Request")
            {
                ViewBag.ErrorMessage = "Incorrect User id or Password";
            }
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                Session["UserID"] = user.UserId.ToString();
                Session["Password"] = user.Password.ToString();
                return RedirectToAction("UserHome", user);
            }
            return View();

        }


        public ActionResult UserHome(UserModel user1)
        {

            user1.UserId = Convert.ToInt32(Session["UserId"]);
            user1.Password = Convert.ToString(Session["Password"]);

            UserMvcController obj = new UserMvcController();
            UserModel user = obj.GetUserDetail(user1).Result;
            if (user.Role == true)
            {
                user.RoleName = "Admin";
            }
            else
            {
                user.RoleName = "Employee";
            }
            //...........creating session
            Session["UserID"] = user.UserId.ToString();
            Session["UserName"] = user.UserName.ToString();
            Session["Role"] = user.Role.ToString();
            Session["Password"] = user.Password.ToString();
            Session["IsDeleted"] = user.IsDeleted.ToString();
            Session["RoleName"] = user.RoleName.ToString();

            ////............updating values
            user.UserId = Convert.ToInt32(Session["UserId"]);
            user.UserName = Convert.ToString(Session["UserName"]);
            user.Role = Convert.ToBoolean(Session["Role"]);
            user.Password = Convert.ToString(Session["Password"]);
            user.IsDeleted = Convert.ToBoolean(Session["IsDeleted"]);
            user.RoleName = Convert.ToString(Session["RoleName"]);
            return View(user);
        }

        private async Task<UserModel> GetUserDetail(UserModel user)
        {

            List<UserModel> users = new List<UserModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Admin/GetAllUsers").ConfigureAwait(false);

            var result = res.Content.ReadAsStringAsync().Result;
            users = JsonConvert.DeserializeObject<List<UserModel>>(result);

            foreach (UserModel u in users)
            {
                if (u.UserId == user.UserId && u.Password.Equals(user.Password))
                {
                    user = u;
                    break;
                }
            }
            return user;
        }

        [HttpGet]
        public async Task<ActionResult> DisplayProducts()
        {

            List<ProductModel> products = new List<ProductModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Admin/GetAllProducts").ConfigureAwait(false);

            var result = res.Content.ReadAsStringAsync().Result;
            products = JsonConvert.DeserializeObject<List<ProductModel>>(result); 

            
            return View(products);
        }

        

        
        public ActionResult Cart(int id) 
        {
            UserMvcController obj = new UserMvcController();
            ProductModel product = obj.GetProductById(id).Result;
            product.Quantity = 1;
            
            return View(product);

        }

        [HttpPost]
        public ActionResult Cart(int id,ProductModel product)
        {
            UserMvcController obj = new UserMvcController();
            ProductModel product1 = obj.GetProductById(id).Result;
           
            product1.Quantity = product.Quantity;
            product1.Price=product.Quantity*product1.Price;
            cartProducts.Add(product1);
            Session["cartProducts"] = cartProducts;
            return RedirectToAction("ViewCart");

        }

        public ActionResult ViewCart()
        {
            return View(cartProducts);
        }

        public ActionResult RemoveCartProduct(int id)
        {
            foreach(ProductModel p in cartProducts)
            {
                if (p.ProductId == id)
                {
                    cartProducts.Remove(p);
                    break;
                }
            }
            return RedirectToAction("ViewCart");
        }



        private async Task<ProductModel> GetProductById(int id)
        {
            ProductModel product = new ProductModel();
            List<ProductModel> products = new List<ProductModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Admin/GetAllProducts").ConfigureAwait(false);

            var result = res.Content.ReadAsStringAsync().Result;
            products = JsonConvert.DeserializeObject<List<ProductModel>>(result);

            foreach(ProductModel p in products)
            {
                if (p.ProductId == id)
                {
                    product = p;
                    break;
                }
            }
            return product;
        }


        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerModel customer)
        {

            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CustomerModel>("User/AddCustomer", customer);
            postTask.Wait();
            var result = postTask.Result;
            var postTask2 = client.PostAsJsonAsync<List<ProductModel>>("User/AddPurchasedProducts", cartProducts);
            postTask2.Wait();
            var result2 = postTask2.Result;

            if (result.IsSuccessStatusCode && result2.IsSuccessStatusCode)
            {
                cartProducts.Clear();
                return RedirectToAction("ViewReciept");
            }
            return View();

        }

        public async Task<ActionResult> ViewReciept()
        {
            RecieptModel reciept = new RecieptModel();
            List<ProductModel> products = new List<ProductModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("User/GetLastReciept").ConfigureAwait(false);

            var result = res.Content.ReadAsStringAsync().Result;
            reciept = JsonConvert.DeserializeObject<RecieptModel>(result);

            
            return View(reciept);
        }

    }
}

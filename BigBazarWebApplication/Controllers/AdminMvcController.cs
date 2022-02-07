using BigBazarWebApplication.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BigBazarWebApplication.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;


namespace BigBazarWebApplication.Controllers
{
    public class AdminMvcController : Controller
    {
        BigBazarApi _api = new BigBazarApi();

        
        public ActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public  ActionResult LoginAdmin(UserModel user)
        {
            
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<UserModel>("Admin/LoginAdmin", user);
            postTask.Wait();
            if (postTask.Result.ReasonPhrase == "Bad Request")
            {
                ViewBag.ErrorMessage = "Incorrect User id or Password";
            }
            var result = postTask.Result;

            if (result.IsSuccessStatusCode )
            {
                Session["UserID"] = user.UserId.ToString();
                Session["Password"] = user.Password.ToString();
                return RedirectToAction("AdminHome", user);
            }
            return View();

        }

       
        public ActionResult AdminHome(UserModel user1)
        {

            user1.UserId = Convert.ToInt32(Session["UserId"]);
            user1.Password = Convert.ToString(Session["Password"]);

            AdminMvcController obj = new AdminMvcController();
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
        public async Task<ActionResult> DisplayUsers() 
        {
            List<UserModel> users = new List<UserModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Admin/GetAllUsers");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                users  = JsonConvert.DeserializeObject<List<UserModel>>(result);
            }
            return View(users);
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(UserModel user)
        {
            
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<UserModel>("Admin/AddUser", user);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("DisplayUsers");
            }
            return View();
        }


        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(ProductModel product)
        {
            product.ChangedQuantity = product.Quantity;
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<ProductModel>("Admin/AddProduct", product);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("DisplayProducts");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DisplayProducts()
        {
            List<ProductModel> products = new List<ProductModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Admin/GetAllProducts");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                products = JsonConvert.DeserializeObject<List<ProductModel>>(result);
            }
            return View(products);
        }


        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryModel category)
        {
            
            HttpClient client = _api.Initial();
            var postTask = client.PostAsJsonAsync<CategoryModel>("Admin/AddCategory", category);
            postTask.Wait();
            var result = postTask.Result;

            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("DisplayCategories");
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> DisplayCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("Admin/GetCategories");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<CategoryModel>>(result);
            }
            return View(categories);
        }

    }
}

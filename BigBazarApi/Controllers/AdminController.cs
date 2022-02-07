using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigBazarServices;
using BigBazarEntities.Entity;
using CustomException;

namespace BigBazarApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        IAdminServices _Services;

        public AdminController(IAdminServices adminServices)
        {
            _Services = adminServices;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                return Ok(await _Services.AddUser(user));
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            try
            {
                return Ok(await _Services.AddCategory(category));
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                return Ok(await _Services.AddProduct(product));
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteUser")]
        public async Task<IActionResult> DeleteUser(User user)
        {
            try
            {
                return Ok(await _Services.DeleteUser(user));
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                return Ok(await _Services.GetAllProducts());
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                return Ok(await _Services.GetAllUsers());
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserDetails")]
        public User GetUserDetails(int id)
        {
           List<User> users= _Services.GetAllUsers().Result;
            User userDetail = new User();
            foreach(User u in users)
            {
                if (u.UserId == id)
                {
                    userDetail.UserId = u.UserId;
                    userDetail.UserName = u.UserName;
                    userDetail.Role = u.Role;
                    userDetail.Password = u.Password;
                    userDetail.IsDeleted = u.IsDeleted;
                    break;
                }
            }
            return  userDetail;
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                return Ok(await _Services.GetCategories());
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProductsByCategory")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            try
            {
                return Ok(await _Services.GetProductsByCategory(categoryId));
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                return Ok(await _Services.GetUserById(id));
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin(User user)
        {
            try
            {
                return Ok(await _Services.LoginAdmin(user));
            }
            catch (SqlException ex)
            { 
                return BadRequest(ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

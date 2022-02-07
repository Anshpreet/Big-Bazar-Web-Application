using BigBazarEntities.Entity;
using BigBazarServices;
using CustomException;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBazarApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        IUserServices _Services;

        public UserController(IUserServices userServices)
        {
            _Services = userServices;
        }

        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(User user)
        {
            try
            {
                return Ok(await _Services.LoginUser(user));
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

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            try
            {
                return Ok(await _Services.AddCustomer(customer));
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

        [HttpPost("AddPurchasedProducts")]
        public async Task<IActionResult> AddPurchasedProducts(List<Product> products)
        {
            try
            {
                return Ok(await _Services.AddPurchasedProducts(products));
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

        [HttpGet("GetLastReciept")]
        public async Task<IActionResult> GetLastReciept()
        {
            try
            {
                return Ok(await _Services.GetLastReciept());
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

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                return Ok(await _Services.GetAllCustomers());
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

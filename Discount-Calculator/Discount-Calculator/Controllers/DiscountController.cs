using Catalog.API.Repositories;
using Discount_Calculator.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Discount.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;

        }
        [HttpGet]
        public IActionResult Calculatetotalprice(int price, int weight, int? discount,string UserName)
        {
            bool isAuthorized= _discountRepository.isAuthorized(UserName);
            if (!isAuthorized)
                return Unauthorized();
            else
            {
                var result = _discountRepository.Calculatetotalprice(price, weight, discount);
                return Ok(result);
            }
        }
        [HttpPost]
        public HttpResponseMessage Login(string Username, string Password)
        {
           return _discountRepository.Login(Username, Password);
        }
        }
}

using Microsoft.AspNetCore.Mvc;
using System;
using IDPDemoApp.Api.Data;
using Microsoft.AspNetCore.Authorization;

namespace IDPDemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsRepository _productsRepository;
        public ProductsController(ProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var products = _productsRepository.GetAll();
                return Ok(products);
            }
            catch (Exception e)
            {
                return Ok(e);
            }

        }
    }
}

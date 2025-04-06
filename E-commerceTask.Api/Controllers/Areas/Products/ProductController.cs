using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static E_commerceTask.Shared.Constants.SD;
using System.Data;

namespace E_commerceTask.Api.Controllers.Areas.Products
{

    [Area(Modules.AdminPanel)]
    [ApiController]
    [ApiExplorerSettings(GroupName = Modules.AdminPanel)]
    [Route("api/v1/")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(new
            {
                message = "Hello from ProductController",
                status = true
            });
        }   
    }
}

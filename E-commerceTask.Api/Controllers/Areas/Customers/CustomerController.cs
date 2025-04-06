using E_commerceTask.Application.DTOs.Requests.Customers;
using E_commerceTask.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using static E_commerceTask.Shared.Constants.SD;

namespace E_commerceTask.Api.Controllers.Areas.Customers
{

    [Area(Modules.AdminPanel)]
    [ApiController]
    [ApiExplorerSettings(GroupName = Modules.AdminPanel)]
    [Route("api/v1/")]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        [HttpGet(ApiRoutes.Customers.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var response = await customerService.GetAllAsync();
            if (response.Check)
            {
                return Ok(response);
            }
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpGet(ApiRoutes.Customers.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await customerService.GetByIdAsync(id);
            if (response.Check)
            {
                return Ok(response);
            }
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPost(ApiRoutes.Customers.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDTO dto)
        {
            var response = await customerService.CreateAsync(dto);
            if (response.Check)
            {
                return Ok(response);
            }
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
    }

}

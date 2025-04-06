using E_commerceTask.Application.DTOs.Requests.Orders;
using E_commerceTask.Application.Interfaces.IServices;
using E_commerceTask.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using static E_commerceTask.Shared.Constants.SD;

namespace E_commerceTask.Api.Controllers.Areas.Orders
{

    [Area(Modules.AdminPanel)]
    [ApiController]
    [ApiExplorerSettings(GroupName = Modules.AdminPanel)]
    [Route("api/v1/")]
    public class OrderController(IOrderService orderService) : ControllerBase
    {
        [HttpGet(ApiRoutes.Orders.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await orderService.GetByIdAsync(id); 
            if (response.Check)
            {
                return Ok(response);
            }
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPost(ApiRoutes.Orders.Create)]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var response = await orderService.CreateAsync(dto);
            if (response.Check)
            {
                return Ok(response);
            }
            else if (!response.Check)
                return StatusCode(statusCode: StatusCodes.Status400BadRequest, response);
            return StatusCode(statusCode: StatusCodes.Status500InternalServerError, response);
        }
        [HttpPost(ApiRoutes.Orders.UpdateStatus)]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id)
        {
            var response = await orderService.UpdateStatusAsync(id);
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

using Microsoft.AspNetCore.Mvc;
using TestWork.Contracts;
using TestWork.Models.DTO;
using TestWork.Models.Request;
using TestWork.Models.Response;

namespace TestWork.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilterController([FromServices] IOrderService orderService) : ControllerBase
    {
        private readonly IOrderService _orderService = orderService;

        [HttpGet()]
        public async Task<IActionResult> GetFilterData([FromQuery] FilterOrderGetRequest request, CancellationToken token)
        {
            ICollection<OrderDTO> orders = [];

            if (!string.IsNullOrWhiteSpace(request.AreaName))
            {
                orders = await _orderService.GetFilteredOrdersByAreaName(request.AreaName, token);
            }
            else if(request.StartDeliveryOrder > request.EndDeliveryOrder)
            {
                return BadRequest();
            }
            else if (request.StartDeliveryOrder != null || request.EndDeliveryOrder != null)
            {
                var start = request.StartDeliveryOrder ?? DateTime.MinValue;
                var end = request.EndDeliveryOrder ?? DateTime.UtcNow;
                orders = await _orderService.GetFilteredOrdersByDateTime(start, end, token);
            }
            else
            {
                orders = await _orderService.GetAllOrders(token);
            }

            return Ok(new FilteredOrdersGetResponse(orders));
        }
    }
}
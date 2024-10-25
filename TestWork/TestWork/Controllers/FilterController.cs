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

        [HttpPost()]
        public async Task<IActionResult> GetFilterData([FromBody] FilterOrderPostRequest request, CancellationToken token)
        {
            ICollection<OrderDTO> orders = [];

            if(string.IsNullOrWhiteSpace(request.СityDistrictName) || request.StartDeliveryOrderDateTime == DateTime.MinValue)
            {
                return BadRequest();
            }

            var start = DateTime.SpecifyKind(request.StartDeliveryOrderDateTime.ToUniversalTime(), DateTimeKind.Unspecified);
            var end = DateTime.SpecifyKind(start.AddMinutes(30), DateTimeKind.Unspecified);
            orders = await _orderService.GetFilteredOrders(request.СityDistrictName, start, end, token);

            return Ok(new FilteredOrdersPostResponse(orders));
        }
    }
}
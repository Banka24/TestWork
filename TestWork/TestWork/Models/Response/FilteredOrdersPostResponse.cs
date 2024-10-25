using TestWork.Models.DTO;

namespace TestWork.Models.Response
{
    public record class FilteredOrdersPostResponse(ICollection<OrderDTO> Orders);
}
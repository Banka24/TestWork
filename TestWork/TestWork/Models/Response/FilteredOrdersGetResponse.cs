using TestWork.Models.DTO;

namespace TestWork.Models.Response
{
    public record class FilteredOrdersGetResponse(ICollection<OrderDTO> Orders);
}
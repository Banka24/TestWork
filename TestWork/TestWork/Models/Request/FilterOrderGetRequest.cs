namespace TestWork.Models.Request
{
    public record class FilterOrderGetRequest(string? AreaName, DateTime? StartDeliveryOrder, DateTime? EndDeliveryOrder);
}
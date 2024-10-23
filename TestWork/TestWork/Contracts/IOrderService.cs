using TestWork.Models.DTO;

namespace TestWork.Contracts
{
    public interface IOrderService
    {
        Task<ICollection<OrderDTO>> GetFilteredOrdersByAreaName(string areaName, CancellationToken token);
        Task<ICollection<OrderDTO>> GetFilteredOrdersByDateTime(DateTime startDeliveryOrder, DateTime endDeliveryOrder, CancellationToken token);
        Task<ICollection<OrderDTO>> GetAllOrders(CancellationToken token);
    }
}
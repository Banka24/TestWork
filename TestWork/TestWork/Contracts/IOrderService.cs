using TestWork.Models.DTO;

namespace TestWork.Contracts
{
    public interface IOrderService
    {
        Task<ICollection<OrderDTO>> GetFilteredOrders(string areaName, DateTime start, DateTime end, CancellationToken token);
    }
}
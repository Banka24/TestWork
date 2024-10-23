using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TestWork.Contracts;
using TestWork.DataAccess;
using TestWork.Models.DTO;
using TestWork.Models.Entity;

namespace TestWork.Services
{
    public class OrderService([FromServices] TestWorkDbContext context) : IOrderService
    {
        private readonly TestWorkDbContext _context = context;

        public async Task<ICollection<OrderDTO>> GetAllOrders(CancellationToken token)
        {
            ICollection<OrderDTO> orders = [];
            try
            {
                orders = await _context.Orders.Select(order => new OrderDTO(order.Area.AreaName, order.DeliveryOrderTime.ToString("yyyy-MM-dd HH:mm:ss"))).ToArrayAsync(token);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return orders;
            }

            Log.Logger.Information($"Было получено {orders.Count} записей");
            return orders;
        }

        public async Task<ICollection<OrderDTO>> GetFilteredOrdersByAreaName(string areaName, CancellationToken token)
        {
            ICollection<OrderDTO> orders = [];

            try
            {
                orders = await _context.Orders.Where(order => order.Area.AreaName.ToLower() == areaName.ToLower())
                    .Select(order => new OrderDTO(order.Area.AreaName, order.DeliveryOrderTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ToArrayAsync(token);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return orders;
            }

            Log.Logger.Information($"Было получено {orders.Count} записей, по району: {areaName}");
            return orders;
        }

        public async Task<ICollection<OrderDTO>> GetFilteredOrdersByDateTime(DateTime startDeliveryOrder, DateTime endDeliveryOrder, CancellationToken token)
        {
            ICollection<OrderDTO> orders = [];

            try
            {
                orders = await _context.Orders
                    .Where(order => order.DeliveryOrderTime >= startDeliveryOrder && order.DeliveryOrderTime <= endDeliveryOrder)
                    .Select(order => new OrderDTO(order.Area.AreaName, order.DeliveryOrderTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ToArrayAsync(token);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return orders;
            }

            Log.Logger.Information($"Было получено {orders.Count} записей, с {startDeliveryOrder} по {endDeliveryOrder}");
            return orders;
        }

        private static void LogException(Exception ex)
        {
            if (ex is FormatException or ArgumentOutOfRangeException)
            {
                Log.Logger.Error(ex.Message, ex.Source);
            }
            else
            {
                Log.Logger.Fatal(ex.Message, ex.Source);
            }
        }
    }
}
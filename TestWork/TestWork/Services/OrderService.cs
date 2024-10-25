using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using TestWork.Contracts;
using TestWork.DataAccess;
using TestWork.Models.DTO;

namespace TestWork.Services
{
    public class OrderService([FromServices] TestWorkDbContext context) : IOrderService
    {
        private const string EXCEPTIONMESSAGE = "Сообщение об ошибке {ExceptionType}: {Message}.\nОбъект, вызвавший исключение: {Source}.\nСтек исключения: {StackTrace}";
        private readonly TestWorkDbContext _context = context;

        public async Task<ICollection<OrderDTO>> GetFilteredOrders(string cityDistrictName, DateTime start, DateTime end, CancellationToken token)
        {
            ICollection<OrderDTO> orders = [];

            try
            {
                var cityDistrict = await _context.СityDistricts.FirstOrDefaultAsync(district => district.СityDistrictName == cityDistrictName, token);

                if(cityDistrict is null)
                {
                    Log.Logger.Warning($"Не был найден район с названием: {cityDistrictName}");
                    return orders;
                }

                orders = await _context.Orders
                    .Where(order => order.СityDistrictId == cityDistrict.СityDistrictId && 
                    order.DeliveryOrderTime >= start && order.DeliveryOrderTime <= end)
                    .Select(order => new OrderDTO(order.СityDistrict.СityDistrictName, order.DeliveryOrderTime.ToString("yyyy-MM-dd HH:mm:ss")))
                    .ToArrayAsync(token);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return orders;
            }

            Log.Logger.Information($"Было получено {orders.Count} записей, по району {cityDistrictName} с {start} по {end}");
            return orders;
        }

        private static void LogException(Exception ex)
        {
            var logEventLevel = ex is FormatException or ArgumentOutOfRangeException ? LogEventLevel.Error : LogEventLevel.Fatal;
            Log.Logger.Write(logEventLevel, ex, EXCEPTIONMESSAGE, ex.GetType().Name, ex.Message, ex.Source, ex.StackTrace);
        }
    }
}
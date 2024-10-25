using Serilog;
using TestWork.Contracts;
using TestWork.DataAccess;
using TestWork.Services;

namespace TestWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Level}] {Message}{NewLine}{Exception}")
                .WriteTo.File("Logs/deliveryLog-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "[{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Host.UseSerilog();

            builder.Services.AddScoped<TestWorkDbContext>();
            builder.Services.AddScoped<IOrderService, OrderService>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            app.Run();
        }
    }
}
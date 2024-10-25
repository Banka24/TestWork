using TestWork.Controllers;

namespace TestTestWork
{
    public class OrderServiceTest
    {
        [Fact]
        public void GetFilteredOrders()
        {
            string cityDistrictName = string.Empty;
            var start = DateTime.Now;
            var end = start.AddMinutes(10);

            var controller = new FilterController();

        }
    }
}
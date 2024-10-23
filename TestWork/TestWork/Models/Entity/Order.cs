namespace TestWork.Models.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public short Weight { get; set; }
        public short AreaId { get; set; }
        public DateTime DeliveryOrderTime { get; set; }
        public Area Area { get; set; }
    }
}
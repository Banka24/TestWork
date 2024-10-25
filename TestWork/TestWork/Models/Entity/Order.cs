namespace TestWork.Models.Entity
{
    public class Order
    {
        public int OrderId { get; set; }
        public short Weight { get; set; }
        public short СityDistrictId { get; set; }
        public DateTime DeliveryOrderTime { get; set; }
        public СityDistrict СityDistrict { get; set; }
    }
}
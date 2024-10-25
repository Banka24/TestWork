namespace TestWork.Models.Entity
{
    public class СityDistrict
    {
        public short СityDistrictId { get; set; }
        public string СityDistrictName { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
    }
}
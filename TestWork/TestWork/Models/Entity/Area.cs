namespace TestWork.Models.Entity
{
    public class Area
    {
        public short AreaId { get; set; }
        public string AreaName { get; set; }
        public ICollection<Order> Orders { get; set; } = [];
    }
}
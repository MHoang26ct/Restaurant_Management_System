public class OrderModel
{
    public string OrderId { get; set; }
    public string CustomerName { get; set; }
    public string TableNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public int TotalItems { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Pending";
}
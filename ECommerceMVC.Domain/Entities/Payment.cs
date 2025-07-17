namespace ECommerceMVC.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0.0m;
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    public int OrderId { get; set; }
    public Order Order { get; set; } = new Order();

    public int PaymentMethodId { get; set; }
    public string TransactionId { get; set; } = string.Empty;
}
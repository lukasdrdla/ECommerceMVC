using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceMVC.Domain.Entities;

public class RefundRequest
{
    public int Id { get; set; }

    public int OrderItemId { get; set; }
    public OrderItem OrderItem { get; set; }

    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public string Reason { get; set; } = string.Empty;

    public RefundStatus Status { get; set; } = RefundStatus.Pending;
}

public enum RefundStatus
{
    Pending,      // Žádost čeká na zpracování
    Approved,     // Žádost byla schválena
    Rejected,     // Žádost byla zamítnuta
    Processed     // Refundace byla provedena
}


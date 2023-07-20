using PayMimi.Domain.Entities;

namespace PayMimi.Domain.Events.Transaction;

public class Created : TransactionEvent
{
    public long CustomerId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
namespace PayMimi.Domain.Events.Transaction;

public interface TransactionEvent
{
    DateTime Date { get; set; }
}
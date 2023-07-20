
using System.Data.SqlTypes;
using PayMimi.Exceptions;

namespace PayMimi.Domain.Entities;

public class Transaction
{
    public long? Id { get; private set; }
    public Customer Customer { get; private set; }
    public TransactionType  Type { get; private set; }
    public decimal Amount { get; private set; }

    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

    public DateTime Date { get; private set; } = DateTime.UtcNow;

    public Transaction()
    {
    }
    
    public Transaction(Customer customer, decimal amount, TransactionType transactionType) : base()
    {
        if (amount <= 0)
        {
            throw new NegativeAmountException("Amount must be greater than zero");
        }

        Customer = customer ?? throw new ArgumentNullException(nameof(customer));
        Amount = amount;
        Type = transactionType;
    }

    public static Transaction CreateIncoming(Customer customer, decimal amount)
    {
        
        return new Transaction(customer, amount, TransactionType.Incoming);
    }
    
    public static Transaction CreateOutgoing(Customer customer, decimal amount)
    {
        return new Transaction(customer, amount, TransactionType.Outgoing);
    }
}
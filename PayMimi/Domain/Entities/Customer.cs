using PayMimi.Domain.ValueObjects;

namespace PayMimi.Domain.Entities;

public class Customer
{
    private readonly Name _name;
    public long? Id { get; init; }
    
    public string Name => _name.FullName;
    
    public Customer(Name name, long? id = null)
    {
        Id = id;
        _name = name;
    }
}
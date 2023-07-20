namespace PayMimi.Domain.ValueObjects;

public class Name
{
    public string FirstName { get; init; }
    public string LastName { get; init; }

    public string FullName => $"{FirstName} {LastName}".Trim();
    
    public Name(string firstName, string lastName)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is not Name other)
        {
            return false;
        }

        return FirstName == other.FirstName && LastName == other.LastName;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(FirstName, LastName);
    }
}
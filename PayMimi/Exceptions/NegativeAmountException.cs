namespace PayMimi.Exceptions;

public class NegativeAmountException : ArgumentException
{
    public NegativeAmountException(string message) : base(message)
    {
    }
}
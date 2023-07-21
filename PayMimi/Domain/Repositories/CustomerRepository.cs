namespace PayMimi.Domain.Repositories;

public class CustomerRepository : ICustomerRepository
{
    public Task<bool> AlreadyExists(string email, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(email == "example@mail.com");
    }
}
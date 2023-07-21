namespace PayMimi.Domain.Repositories;

public interface ICustomerRepository
{
    Task<bool> AlreadyExists(string email, CancellationToken cancellationToken = default);
}
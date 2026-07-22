using BeautySalonBooking.Domain.PersonAggregate.Entities;

namespace BeautySalonBooking.Domain.PersonAggregate.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(long id, CancellationToken cancellationToken);
    Task<Person?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
    Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken);
    Task AddAsync(Person person, CancellationToken cancellationToken);
    void Update(Person person, CancellationToken cancellationToken);
    void Remove(Person person, CancellationToken cancellationToken);
}

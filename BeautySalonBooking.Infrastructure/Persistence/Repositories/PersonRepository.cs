using BeautySalonBooking.Domain.PersonAggregate.Entities;
using BeautySalonBooking.Domain.PersonAggregate.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BeautySalonBooking.Infrastructure.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly BeautyDbContext _context;

    public PersonRepository(BeautyDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Persons
            .Include(p => p.Users)
            .ThenInclude(u => u.UserRole)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(Person person, CancellationToken cancellationToken)
    {
       var id = await _context.Persons.AddAsync(person, cancellationToken);
    }

    public void Update(Person person, CancellationToken cancellationToken)
    {
        _context.Persons.Update(person);
    }

    public Task<Person?> GetByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNationalCodeAsync(string nationalCode, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Remove(Person person, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
